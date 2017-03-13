using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace MatrizInversaGaussJordan
{
    class Program
    {
        
        //el determinante distinto de 0 indica si la matriz posee inversa
        public static List<List<int>> crearMatrizIdentidad(int orden)
        {
            List<List<int>> matrizIdentidad = new List<List<int>>();
            for (int x = 0; x < orden; x++)
            {
                matrizIdentidad.Add(new List<int>());
                for (int y = 0; y < orden; y++)
                {
                    if (x == y)
                        matrizIdentidad[x].Add(1);
                    else
                        matrizIdentidad[x].Add(0);

                }
            }
            return matrizIdentidad;
        }

        public static List<List<int>> construirMatrizAumentada(List<List<int>> matrizInvertir, int orden)
        {
            List<List<int>> matrizAumentada = new List<List<int>>(); //crear memoria para matriz Aumentada
            List<List<int>> matrizIn = crearMatrizIdentidad(orden); //mediante funcion retornar una matriz identidad

            for (int x = 0; x < orden; x++)   //filas de la matriz aumentada de tamaño orden
            {
                matrizAumentada.Add(new List<int>());

                for (int y = 0; y < orden * 2; y++)   //columnas de la matriz aumentada
                {
                    if (y <= orden - 1)
                        matrizAumentada[x].Add(matrizInvertir[x][y]);
                    else
                        matrizAumentada[x].Add(matrizIn[x][y-orden]);
                }
            }
            return matrizAumentada; 
        }

        public static List<List<int>> operacionDivisor(List<List<int>> matrizAumentada, int fila, int divisor)
        {
            for (int i = 0; i < matrizAumentada.Count; i++)
            {
                for (int j = 0; j < matrizAumentada.Count * 2; j++)
                {
                    if (i == fila)
                    {
                        matrizAumentada[i][j] = matrizAumentada[i][j] * 1 / divisor;
                        int pruebaDivisor = matrizAumentada[i][j];
                    }
                }
            }
                return matrizAumentada;
        }

        public static List<List<int>> operacionResta(List<List<int>> matrizAumentada, int filaModificar, int filaUno, int numero)
        {
            //x = numero, multiplicar x por fila del uno - fila donde quiero el 0
            for (int i = 0; i < matrizAumentada.Count; i++)
            {
                for (int j = 0; j < matrizAumentada.Count * 2; j++)
                {
                    if (i == filaModificar)
                    {
                        matrizAumentada[i][j] = (numero * matrizAumentada[filaUno][j]) - matrizAumentada[i][j];
                        int pruebaResta = matrizAumentada[i][j];
                    }
                }
            }
            return matrizAumentada;
        }

        public static List<List<int>> operacionFilas(List<List<int>> matrizAumentada, int filaModificar, int filaCambio)
        {
            for (int i = 0; i < matrizAumentada.Count; i++)
            {
                for (int j = 0; j < matrizAumentada.Count * 2; j++)
                {
                    if (i == filaModificar)
                    {
                        int temp = matrizAumentada[i][j];
                        matrizAumentada[i][j] = matrizAumentada[filaCambio][j];
                        matrizAumentada[filaCambio][j] = temp;
                    }
                }
            }
            return matrizAumentada;
        }

        public static List<List<int>> construirInversa(List<List<int>> matrizAumentada)
        {
            //el recorrido lo debe hacer por columnas en vez de filas

            for (int col = 0; col < matrizAumentada.Count / 2; col++)
            {
                bool busquedaUno = false;
                for (int fil = 0; fil < matrizAumentada.Count; fil++)
                {
                    
                    if (!busquedaUno)
                    {
                        int prueba = matrizAumentada[fil][col];
                        if (col == fil) //si se encuentra en una diagonal
                        {
                            if (matrizAumentada[fil][col] == 0)
                            {
                                //cambio de filas con la de abajo o la que no contenga otro cero
                                //matrizAumentada = operacionFilas(matrizAumentada,,); //aqui se debe condicionar cuando debe ser una fila u otra
                            }
                            else
                            {
                                //se divide toda la fila de x * 1/x
                                matrizAumentada = operacionDivisor(matrizAumentada, fil, matrizAumentada[fil][col]);
                            }
                        }
                        Write(matrizAumentada[fil][col]);
                        fil = -1;
                        busquedaUno = true;
                    }          
                    else //solo se debe convertir a cero
                    {
                        //x = numero, multiplicar x por fila del uno - fila donde quiero el 0
                        int prueba = matrizAumentada[fil][col];
                        if (matrizAumentada[fil][col] != 1)
                            matrizAumentada = operacionResta(matrizAumentada,fil ,col ,matrizAumentada[fil][col]);
                        Write(matrizAumentada[fil][col]);
                    }
                    
                }
                WriteLine();
            }
            return matrizAumentada;
        }

        static public List<List<int>> construirMatrizNormal()
        {
            List<List<int>> matriz = new List<List<int>>();
            for (int x = 0; x < 3; x++)
            {
                matriz.Add(new List<int>());
                for (int y = 0; y < 3; y++)
                {
                    matriz[x].Add(0);
                }                 
            }
            return matriz;            
        }

        static void Main(string[] args)
        {
            int orden = 3;
            List<List<int>> matriz = construirMatrizNormal();

            matriz[0][0] = 1;
            matriz[0][1] = 0;
            matriz[0][2] = 2;

            matriz[1][0] = 2;
            matriz[1][1] = -1;
            matriz[1][2] = 3;

            matriz[2][0] = 4;
            matriz[2][1] = 1;
            matriz[2][2] = 8;

            List<List<int>> aum = construirMatrizAumentada(matriz, orden);
            //aum = operacionDivisor(aum, 2, 8);
            //aum = operacionResta(aum, 1, 0, 2);
            //aum = operacionFilas(aum, 1, 2);
            aum = construirInversa(aum);

            for (int i = 0; i < aum.Count; i++)
            {
                for (int j = 0; j < aum.Count * 2; j++)
                {
                    Write(aum[i][j] + ", ");
                }
                WriteLine();
            }

            ReadKey();
        }
    }
}
