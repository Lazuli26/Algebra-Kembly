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
        public static List<List<float>> crearMatrizIdentidad(int orden)
        {
            List<List<float>> matrizIdentidad = new List<List<float>>();
            for (int x = 0; x < orden; x++)
            {
                matrizIdentidad.Add(new List<float>());
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

        public static List<List<float>> construirMatrizAumentada(List<List<float>> matrizInvertir, int orden)
        {
            List<List<float>> matrizAumentada = new List<List<float>>(); //crear memoria para matriz Aumentada
            List<List<float>> matrizIn = crearMatrizIdentidad(orden); //mediante funcion retornar una matriz identidad

            for (int x = 0; x < orden; x++)   //filas de la matriz aumentada de tamaño orden
            {
                matrizAumentada.Add(new List<float>());

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

        public static List<List<float>> operacionDivisor(List<List<float>> matrizAumentada, int fila, float divisor)
        {
            Write("divide la fila " + (fila + 1).ToString() + " entre " + divisor.ToString() + "\n");
            for (int i = 0; i < matrizAumentada.Count; i++)
            {
                for (int j = 0; j < matrizAumentada.Count * 2; j++)
                {
                    if (i == fila)
                    {
                        matrizAumentada[i][j] = matrizAumentada[i][j] / divisor;
                    }
                }
            }
                return matrizAumentada;
        }

        public static List<List<float>> operacionResta(List<List<float>> matrizAumentada, int filaModificar, int filaUno, float numero)
        {
            Write("Resta " + numero.ToString()+" veces la fila " + (filaUno+1).ToString() + " a la fila " + (filaModificar+1).ToString()+"\n");
            //x = numero, multiplicar x por fila del uno - fila donde quiero el 0
            for (int i = 0; i < matrizAumentada.Count; i++)
            {
                for (int j = 0; j < matrizAumentada.Count * 2; j++)
                {
                    if (i == filaModificar)
                    {
                        matrizAumentada[i][j] = matrizAumentada[i][j] - (numero * matrizAumentada[filaUno][j]);
                    }
                }
            }
            return matrizAumentada;
        }

        public static List<List<float>> operacionFilas(List<List<float>> matrizAumentada, int filaModificar, int filaCambio)
        {
            for (int i = 0; i < matrizAumentada.Count; i++)
            {
                for (int j = 0; j < matrizAumentada.Count * 2; j++)
                {
                    if (i == filaModificar)
                    {
                        float temp = matrizAumentada[i][j];
                        matrizAumentada[i][j] = matrizAumentada[filaCambio][j];
                        matrizAumentada[filaCambio][j] = temp;
                    }
                }
            }
            return matrizAumentada;
        }
        
        public static List<List<float>> construirInversa(List<List<float>> matrizAumentada)
        {
            //el recorrido lo debe hacer por columnas en vez de filas
            for (int col = 0; col < matrizAumentada[0].Count / 2; col++)
            {
                if(matrizAumentada[col][col] == 0)
                {
                    //cambio de filas con la de abajo o la que no contenga otro cero
                    //matrizAumentada = operacionFilas(matrizAumentada,,); //aqui se debe condicionar cuando debe ser una fila u otra
                    int xcambio = col + 1;
                    while (true)
                    {
                        if (xcambio == col)
                        {
                            Write("Matriz invalidada: columna de 0 \n");
                            return matrizAumentada;
                        }
                        if (matrizAumentada[xcambio][col] != 0)
                        {
                            operacionFilas(matrizAumentada, col, xcambio);
                            col = 0;
                        }

                        else
                        {
                            xcambio=(xcambio+1)%matrizAumentada.Count;
                        }
                    }
                }
                else
                {
                    //se divide toda la fila de x * 1/x
                    if(matrizAumentada[col][col] != 1)
                    {
                        matrizAumentada = operacionDivisor(matrizAumentada, col, matrizAumentada[col][col]);
                        Write("(" + (col + 1).ToString() + "," + (1 + col).ToString() + ")" + ":" + matrizAumentada[col][col]);
                        imprimir(matrizAumentada);
                        WriteLine();
                    }
                }
                for (int fil = 0; fil < matrizAumentada.Count; fil++)
                {
                    //solo se debe convertir a cero
                    //x = numero, multiplicar x por fila del uno - fila donde quiero el 0
                    if (fil != col)
                    {
                        if(matrizAumentada[fil][col] != 0)
                        {
                            matrizAumentada = operacionResta(matrizAumentada, fil, col, matrizAumentada[fil][col]);
                            Write("(" + (fil + 1).ToString() + "," + (1 + col).ToString() + ")" + ":" + matrizAumentada[fil][col]);
                            imprimir(matrizAumentada);
                            WriteLine();
                        }
                    }
                }
                WriteLine();
            }
            return matrizAumentada;
        }

        static public List<List<float>> construirMatrizNormal()
        {
            List<List<float>> matriz = new List<List<float>>();
            for (int x = 0; x < 3; x++)
            {
                matriz.Add(new List<float>());
                for (int y = 0; y < 3; y++)
                {
                    matriz[x].Add(0);
                }                 
            }
            return matriz;            
        }

        static void imprimir(List<List<float>> aum)
        {
            WriteLine();
            for (int i = 0; i < aum.Count; i++)
            {
                for (int j = 0; j < aum.Count * 2; j++)
                {
                    Write(aum[i][j] + ", ");
                }
                WriteLine();
            }
        }
        static void Main(string[] args)
        {
            int orden = 3;
            List<List<float>> matriz = construirMatrizNormal();

            matriz[0][0] = 1;
            matriz[0][1] = 2;
            matriz[0][2] = -1;

            matriz[1][0] = 0;
            matriz[1][1] = -5;
            matriz[1][2] = 3;

            matriz[2][0] = 3;
            matriz[2][1] = 1;
            matriz[2][2] = 2;

            List<List<float>> aum = construirMatrizAumentada(matriz, orden);
            //aum = operacionDivisor(aum, 2, 8);
            //aum = operacionResta(aum, 1, 0, 2);
            //aum = operacionFilas(aum, 1, 2);
            aum = construirInversa(aum);

            imprimir(aum);
            ReadKey();
        }
    }
}
