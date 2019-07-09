using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppNumeros
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

           
        }

        private void BtnVerificar_Clicked(object sender, EventArgs e)
        {
            if (entNumero.Text != "")
            {
                bool numeroFeliz = ConsultarNumeroFeliz(int.Parse(entNumero.Text));
                bool numeroSortudo = ConsultarNumeroSortudo(int.Parse(entNumero.Text));

                if (numeroFeliz == true && numeroSortudo == true)
                {
                    DisplayAlert("Resultado", "Número Sortudo e Feliz.", "OK");
                }
                else if (numeroFeliz == false && numeroSortudo == false)
                {
                    DisplayAlert("Resultado", "Número Não-Sortudo e Não-Feliz.", "OK");
                }
                else if (numeroFeliz == false && numeroSortudo == true)
                {
                    DisplayAlert("Resultado", "Número Sortudo e Não-Feliz.", "OK");
                }
                else if (numeroFeliz == true && numeroSortudo == false)
                {
                    DisplayAlert("Resultado", "Número Não-Sortudo e Feliz.", "OK");
                }
            }
            else
            {
                DisplayAlert("Resultado", "Digite algum Valor!", "OK");
            }
        }

        //------------------------------//
        //---- Números Sortudos --------//
        //------------------------------//
        public static bool ConsultarNumeroSortudo(int numeroDigitado)
        {
            bool resultado = false;
            List<int> listaNumerosSortudos = new List<int>();
            //Separa os Impares
            listaNumerosSortudos = SepararNumerosImpares();

            //Nós removemos então todos os números com posição múltipla de 3 
            listaNumerosSortudos = SepararNumerosRemoverPosicao(listaNumerosSortudos, 3);

            //Nós removemos então todos os números com posição múltipla de 7 
            listaNumerosSortudos = SepararNumerosRemoverPosicao(listaNumerosSortudos, 7);
            foreach (int item in listaNumerosSortudos)
            {
                if (item == numeroDigitado)
                {
                    resultado = true;
                    break;
                }
            }

            return resultado;
        }

        public static List<int> SepararNumerosImpares()
        {
            int a = 1;
            List<int> Numeros = new List<int>();
            while (a <= 100)
            {
                if (a % 2 != 0)
                    Numeros.Add(a);
                a++;
            }
            return Numeros;
        }

        public static List<int> SepararNumerosRemoverPosicao(List<int> listaNumerosSortudos, int sequencia)
        {
            int index = 1;
            int indexSequencia = 1;

            foreach (int item in listaNumerosSortudos.ToList())
            {
                if (index == sequencia * indexSequencia)
                {
                    listaNumerosSortudos.Remove(item);
                    indexSequencia++;
                }
                index++;
            }

            return listaNumerosSortudos;
        }

        //------------------------------//
        //---- Números Felizes ---------//
        //------------------------------//
        public static bool ConsultarNumeroFeliz(int numeroDigitado)
        {
            bool resultado = false;
            List<int> listaDeNumerosSeparados = new List<int>();
            listaDeNumerosSeparados = SepararNumero(numeroDigitado);
            for (int i = 0; i <= 100 && resultado != true; i++)
            {
                int ResultadoCalculo = CalcularValoresAoQuadrado(listaDeNumerosSeparados);
                if (ResultadoCalculo == 1)
                {
                    resultado = true;
                }
                else
                    listaDeNumerosSeparados = SepararNumero(ResultadoCalculo);
            }
            return resultado;
        }

        //Separa Digitos
        public static List<int> SepararNumero(int numeroDigitado)
        {
            List<int> digitos = new List<int>();
            while (numeroDigitado != 0)
            {
                digitos.Add(numeroDigitado % 10);
                numeroDigitado = numeroDigitado / 10;
            }
            return digitos;
        }

        //Calcula Valores ao quadrado
        public static int CalcularValoresAoQuadrado(List<int> listaDeNumerosSeparados)
        {
            int resultado = 0;
            foreach (int item in listaDeNumerosSeparados)
            {
                resultado += item * item;
            }
            return resultado;
        }
    }
}
