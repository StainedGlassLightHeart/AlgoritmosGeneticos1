using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGprimero1
{
    class Program
    {
        //UNIVERSIDAD AUTONOMA DEL ESTADO DE MEXICO
        //CREADOR: LAURA JESSICA FABIOLA JUAREZ ESQUIVEL
        //PROFESOR: ASDRUBAL LOPEZ CHAU
        //DESCRIPCION:
        //Encuentra el mínimo de la función f(x)=cos(x)*e^{-pi*x/100} en el intervalo de x [-10 a 10]
        //USANDO ALGORITMOS GENETICOS
        //N = 100, Total de generaciones = 500, Mecanismo de selección: Ruleta, Mutación 10%.
        
        public static ArrayList Individuos = new ArrayList();
        public static ArrayList AptitudesMax = new ArrayList();
        public static ArrayList AptitudesMin = new ArrayList();
        public static ArrayList Elejidos = new ArrayList();//En este vector se almacenan los valores elejidos
        public static int Nmaximo = 100;//Para obtener la aptitud menor se resta con respecto a un numero maximo
        public static Random rnd = new Random();
        public static int NIndividuois = 5;//numero total de individuos

        static void Main(string[] args)
        {
            Class1 ini = new Class1();
        }

    }
    }
  
    

