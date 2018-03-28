using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGprimero1
{

    class Class1
    {
        public static ArrayList Individuos = new ArrayList();
        public static ArrayList AptitudesMax = new ArrayList();
        public static ArrayList AptitudesMin = new ArrayList();
        public static ArrayList Elejidos = new ArrayList();//En este vector se almacenan los valores elejidos
        public static int Nmaximo = 100;//Para obtener la aptitud menor se resta con respecto a un numero maximo
        public static Random rnd = new Random();
        public static int Porcentajemutacion = 20;//porcentaje o numero de veces que se mutara la poblacion
        public static int NIndividuois = 100;//numero total de individuos por generacion
        public static int NGeneraciones = 500;//Las veces que habra generaciones
        public static int NumerodeGeneracion = 1;

       public Class1()
        {
            //-------------------------------------------------------------------------------
            //----------------------PASO 1, GENERAR LA POBLACION-----------------------
            //-------------------------------------------------------------------------------
            //-------------------------------------------------------------------------------
            //primero se crea la primer poblacion (100 individuos) con valores de gen aleatorios
            //Console.WriteLine("-------------------------Paso1------------");
            Console.WriteLine("------------------------Primera Generacion------------");
            for (int i = 0; i < NIndividuois; i++)
            {
                int numero = rnd.Next(-10, 11); //con valores de entre -10 y 10
               // Console.WriteLine(numero);
                Individuos.Add(numero);//el id dentro del array list identifica a los individuos
            }
            // Impr(Individuos);

            //recursion!!!!!!!!!!!!!!!!!!!!!!!warning
            Iteraciones();//<<<<<<<<<<<<
            Console.ReadLine();


        }
        public void Iteraciones()
        {
            if (NumerodeGeneracion > NGeneraciones)
            {
                Console.WriteLine("---------------------------------------------------------");
                Console.WriteLine("Numero de generaciones completadas");
                Console.WriteLine("generacion");
                Impr(Individuos);
                Console.WriteLine("---------------------------------------------------------");
                Console.WriteLine("Evaluaciones");
                for (int i = 0; i < NIndividuois; i++)
                {
                    Evaluacion((int)Individuos[i]);
                }
                Impr(AptitudesMin);
                double cubeta = 0;
                int indicemenor=0;
                for(int i = 0; i < NIndividuois; i++)
                {
                    if ((int)Individuos[i] < cubeta)
                    {
                        cubeta = (int)Individuos[i];indicemenor = i;
                    }
                }
                Console.WriteLine("Menor valor optenido en la funcion:"+AptitudesMin[indicemenor]);
                Console.WriteLine("para el Individuo:"+Individuos[indicemenor]);
                Console.WriteLine("---------------------------------------------------------");


            }
            else
            {
            
                if (NumerodeGeneracion % Porcentajemutacion == 0)//toca mutacion!!!!!
                {
                    Console.WriteLine("mutacion");
                    int numero = rnd.Next(0, NIndividuois); //con valores de entre -10 y 10
                    Individuos[numero] = (int)Individuos[numero] + 1;
                }
                Console.WriteLine("generacion numero: " + NumerodeGeneracion);
                //Impr(Individuos);//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<Imprime individuos
                //-------------------------------------------------------------------------------
                //----------------------PASO 2, FUNCION DE APTITUD-----------------------
                //-------------------------------------------------------------------------------
                //Se evaluan los individuos con respecto a la funcion
                //Console.WriteLine("-------------------------Paso2------------");
                for (int i = 0; i < NIndividuois; i++)
                    {
                        Evaluacion((int)Individuos[i]);
                    }
                    //Impr(AptitudesMin);
                    //Impr(AptitudesMax);
                    //-------------------------------------------------------------------------------
                    //----------------------PASO 3, Funicon de seleccion con ruleta-----------------------
                    //-------------------------------------------------------------------------------
                    //Con respecto a su aptitud se realiza una seleccion, Se utiliza el metodo ruleta
                    //Console.WriteLine("-------------------------Paso3------------");
                    Ruleta(AptitudesMax);
                    //-------------------------------------------------------------------------------
                    //----------------------PASO 4, Cruza--------------------------------------------
                    //-------------------------------------------------------------------------------
                    //Se crea la nueva generacion
                    NumerodeGeneracion++;
                    Individuos.Clear();
                    //Console.WriteLine("-------------------------Paso4------------");
                    for (int i = 1; i < NIndividuois; i++)
                    {
                        DtoBy((int)Elejidos[i - 1], (int)Elejidos[i]);
                        i++;
                    }
                    
                //DtoBy(8,-4);
                

                    Iteraciones();
                    
                
            }
           
        }





//------------------------------------------------------------------------Evalua la funcion------------------------------------------------------------------------------
        public static void Evaluacion(int num)
        {
            /*double fin1 = -(Math.PI * num / 100);
            double fin2 = Math.Exp(fin1); 
            double fin3 = Math.Cos(num) * fin2;*/

            //Se evalua el numero con respecto a la funcion f(x)=cos(x)*e^{-pi*x/100}
            double fun = Math.Cos(num) * Math.Exp(-(Math.PI * num / 100));
            AptitudesMin.Add(fun);
            double Aptitud = Nmaximo - fun;//Para obtener la aptitud menor se resta con respecto a un numero maximo
            AptitudesMax.Add(Aptitud);
        }
//---------------------------------------------------------------------Funcion de seleccion ruleta---------------------------------------------------------------------
        public static void Ruleta(ArrayList list)
        {
            double Promedio = 0.0;
            for (int i = 0; i < NIndividuois; i++)
            {//Se calcula el promedio de las aptitudes de los individuos
                Promedio = Promedio + (double)list[i];
            }
            Promedio = Promedio / NIndividuois;
            //Console.WriteLine(Promedio);

            double[] Ve = new double[NIndividuois];
            double T = 0.0;
            for (int i = 0; i < NIndividuois; i++)
            {//Se obtienen valores Ve tales que: Ve(1)= aptitud(1)*promedio
                Ve[i] = (Promedio * (double)list[i]);
                T = T + Ve[i];//se obtiene la sumatoria de los valores esperados
            }
            /*Console.WriteLine("Ve=");
            for (int i = 0; i < NIndividuois; i++)
            {
                Console.WriteLine(Ve[i]);
            }
            Console.WriteLine("T= " + T);
            */
            double sumatoria = 0.0;

            for (int i = 0; i < NIndividuois; i++)//Se repiten N veces la seleccion y el valor pseudoaleatorio
            {
                double r = rnd.NextDouble() * (T + 1.0 - 0.0);//con valores de entre 0 y T
                //Console.WriteLine(r + "de " + i);

                for (int j = 0; j < NIndividuois; j++)
                {
                    sumatoria = sumatoria + Ve[j];//Elegir al individuo sumando las Ve hasta cumplir la condición
                    if (sumatoria >= r)
                    {
                        Elejidos.Add(Individuos[j]);//El individuo seleccionado pasa a la ronda
                        sumatoria = 0.0;
                        break;
                    }
                }
            }
           // Impr(Elejidos);
            // Console.ReadLine();
        }
//--------------------Funcion conversion binaria decimal---------------------------------------------------------------------
        public static void DtoBy(int señorX, int señoraY)
        {
            int longBits = 9;
            int[] mom = new int[longBits];
            int[] dad = new int[longBits];
            //conversion a binario 1
            if (señorX < 0)//si es negativo se coloca un 1 en primer lugar
            {
                señorX = señorX * -1;
                dad[0] = 1;
            }
            else
            {
                dad[0] = 0;
            }

            string binary = Convert.ToString(señorX, 2);
            //Console.WriteLine(binary);
            var chars = binary.ToCharArray();
            for (int i = 1; i <= chars.Length; i++)
            {
                if (chars[chars.Length - i].Equals('0'))
                {
                    dad[longBits - i] = 0;
                }
                else
                {
                    dad[longBits - i] = 1;
                }
            }
            /*for (int i = 0; i < longBits; i++)
            {
                Console.Write(dad[i]);
            }*/
            int[] grayDad = gray(longBits, dad);//conversion a gray 1*****<<<<<<<<<<<<<
            /*Console.Write("dadgray");
            for (int i = 0; i < longBits; i++)
                {
               Console.Write(grayDad[i]);
               }
            Console.WriteLine(" ");*/

            //conversion a binario de la madre

            if (señoraY < 0)//si es negativo se coloca un 1 en primer lugar
            {
                señoraY = señoraY * -1;
                mom[0] = 1;
            }
            else
            {
                mom[0] = 0;
            }

            string binary2 = Convert.ToString(señoraY, 2);
            //Console.WriteLine(binary2);
            var chars2 = binary2.ToCharArray();
            for (int i = 1; i <= chars2.Length; i++)
            {
                if (chars2[chars2.Length - i].Equals('0'))
                {
                    mom[longBits - i] = 0;
                }
                else
                {
                    mom[longBits - i] = 1;
                }
            }
            /*for (int i = 0; i < longBits; i++)
            {
                Console.Write(mom[i]);
            }*/
            int[] grayMom = gray(longBits, mom); //conversion a gray 2 * ****<<<<<<<<<<<<<
             /*Console.Write("momgray");
            for (int i = 0; i < longBits; i++)
            {
                Console.Write(grayMom[i]);
            }
            Console.WriteLine(" ");*/
            //ahora tengo grayDad y grayMom, los cruzo!!!...........................................
            int[] cubetaD = grayDad;
            int[] cubetaM = grayMom;
            int[] cubetaD2 = new int[longBits];
            int[] cubetaM2 = new int[longBits];

            for (int i = 0; i < 7; i++)
            {
                cubetaD2[i] = cubetaM[i];
                cubetaM2[i] = cubetaD[i];

            }
            for(int i = 7; i < longBits; i++)
            {
                cubetaD2[i] = cubetaD[i];
                cubetaM2[i] = cubetaM[i];
            }
            grayDad = cubetaD2;
            grayMom = cubetaM2;

            /*Console.WriteLine("Dadgraynew");
            for (int i = 0; i < longBits; i++)
            {
                Console.Write(grayDad[i]);
            }
            Console.WriteLine(" ");
            Console.WriteLine("MomGraynew");
            for (int i = 0; i < longBits; i++)
            {
                Console.Write(grayMom[i]);
            }
            Console.WriteLine(" ");
            */

            //convierto de gray a binario<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
            int[] BinarioMomnw = grayInverso(longBits, grayMom);//conversion a gray inverso
            /*Console.WriteLine("MomBinariohija");
            for (int i = 0; i < longBits; i++)
            {
                Console.Write(BinarioMomnw[i]);
            }
            Console.WriteLine(" ");*/
            int[] binarioDadw = grayInverso(longBits, grayDad);//conversion a gray inverso
            /*Console.WriteLine("DadBinariohijo");
            for (int i = 0; i < longBits; i++)
            {
                Console.Write(binarioDadw[i]);
            }
            Console.WriteLine(" ");*/
            //convierto de binario a decimal<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
            int nuevoGenhijo = GetValue(binarioDadw, longBits);
            int nuevoGenhija = GetValue(BinarioMomnw, longBits);
            //Console.WriteLine("valor hijo " + nuevoGenhijo);
            //Console.WriteLine("valor hija " + nuevoGenhija);
            if (nuevoGenhija < -10)
            {
                nuevoGenhija=nuevoGenhija + 2;
            }
            if (nuevoGenhijo < -10)
            {
                nuevoGenhijo=nuevoGenhijo +2;
            }
            Individuos.Add(nuevoGenhijo);
            Individuos.Add(nuevoGenhija);


        }
////-------------------------------------------------en este caso se traduce el cromosoma de binario a codigo gray---------------------------------------------------------------
        public static int[] gray(int longBits, int[] gen)
        {//esto se debe a que la semejansas son mas notorias entre cromosomas en codigo gray a cromosomas en codigo binario
            int[] genGray;

            genGray = new int[longBits];//nuevo cromosoma a grey

            genGray[0] = gen[0];//en la transformacion de binario a gray el primer elemento pasa igual

            for (int j = 1; j < longBits; j++)//for para transformar de binario a gray
            {
                genGray[j] = gen[j] ^ gen[j - 1];// se realiza la operacion XOR 
            }

            return genGray;
        }
////-------------------------------------------------en este caso se traduce el cromosoma de binario a codigo gray---------------------------------------------------------------
        public static int[] grayInverso(int longBits, int[] gen)
        {//esto se debe a que la semejansas son mas notorias entre cromosomas en codigo gray a cromosomas en codigo binario
            int[] genGray;

            genGray = new int[longBits];//nuevo cromosoma a grey

            genGray[0] = gen[0];//en la transformacion de gray a binario el primer elemento pasa igual

            for (int j = 1; j < longBits; j++)//for para transformar de binario a gray
            {
                genGray[j] = genGray[j-1] ^ gen[j];// se realiza la operacion XOR 
            }

            return genGray;
        }
        //----------------------------------------------------------------------Imprimir el array List------------------------------------------------------------------------
        public static void Impr(ArrayList list)
        {
            Console.WriteLine("");
            for (int i = 0; i < NIndividuois; i++)
            {
                Console.WriteLine(list[i]);
            }
        }
//--------------------------------------------------------------------------Obtengo el valor en binario----------------------------------
        public static int GetValue(int[] gen,int longBits)
        {//get value optiene el valor real (fenotipo) del cromosoma
            //para este caso el gen representa un numero en binario
            //el fenotipo se traduce a decimal
            int sum = 0;
            for (int j = 1; j < longBits; j++)//for para obtener el valor
            {
                sum = (gen[j] * (int)Math.Pow(2, longBits - j - 1)) + sum;
            }
            if (gen[0] == 1)//para todo numero binario negativo
            {
                sum = sum * -1;
            }
            return sum;
        }
    }

}
