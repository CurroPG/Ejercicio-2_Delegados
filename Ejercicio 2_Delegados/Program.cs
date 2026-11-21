using System;

namespace EjerciciosDelegados
{
    // Delegados que usan los ejercicios
    delegate int Operacion(int x, int y);           // Ejercicios 1 a 5
    delegate int Operar(int x, int y);              // Ejercicio 6
    delegate int OperarArray(int[] numeros);        // Ejercicio 7
    delegate int Transformar(int x);                // Ejercicio 8
    delegate int OperacionCallback(int x, int y);   // Ejercicio 9
    delegate double Calculo(double x);              // Ejercicio 10

    internal class Program
    {
        static void Main(string[] args)
        {
            Ejercicio1();
            Ejercicio2();
            Ejercicio3();
            Ejercicio4();
            Ejercicio5();
            Ejercicio6();
            Ejercicio7();
            Ejercicio8();
            Ejercicio9();
            Ejercicio10();

            Console.ReadKey();
        }

        // Métodos de los ejercicios 1 a 5
        static int Sumar(int a, int b) => a + b;
        static int Restar(int a, int b) => a - b;
        static int Multiplicar(int a, int b) => a * b;
        static int Dividir(int a, int b) => a / b;

        // 1. Delegado básico
        static void Ejercicio1()
        {
            Console.WriteLine("\n--- Ejercicio 1 ---");
            Operacion op = Sumar;
            Console.WriteLine("Suma: " + op(5, 3));
        }

        // 2. Múltiples métodos: el mismo delegado apunta primero a Sumar y luego a Restar
        static void Ejercicio2()
        {
            Console.WriteLine("\n--- Ejercicio 2 ---");
            Operacion op = Sumar;
            Console.WriteLine("Suma: " + op(5, 3));

            op = Restar;
            Console.WriteLine("Resta: " + op(5, 3));
        }

        // 3. Multicast: el delegado guarda los tres métodos a la vez
        static void Ejercicio3()
        {
            Console.WriteLine("\n--- Ejercicio 3 ---");
            Operacion op = Sumar;
            op += Restar;
            op += Multiplicar;

            foreach (Operacion metodo in op.GetInvocationList())
            {
                Console.WriteLine("Resultado: " + metodo(5, 3));
            }
        }

        // 4. Añadimos Dividir y controlamos la división entre cero
        static void Ejercicio4()
        {
            Console.WriteLine("\n--- Ejercicio 4 ---");
            Operacion op = Sumar;
            op += Restar;
            op += Multiplicar;
            op += Dividir;

            int a = 10;
            int b = 0;

            foreach (Operacion metodo in op.GetInvocationList())
            {
                try
                {
                    Console.WriteLine("Resultado: " + metodo(a, b));
                }
                catch (DivideByZeroException)
                {
                    Console.WriteLine("Error: no se puede dividir entre cero");
                }
            }
        }

        // 5. Método que recibe un delegado y dos enteros, y muestra el resultado
        static void Ejercicio5()
        {
            Console.WriteLine("\n--- Ejercicio 5 ---");
            MostrarResultado(Sumar, 5, 3);
            MostrarResultado(Restar, 5, 3);
            MostrarResultado(Multiplicar, 5, 3);
        }

        static void MostrarResultado(Operacion op, int a, int b)
        {
            Console.WriteLine("Resultado: " + op(a, b));
        }

        // 6. Delegado Operar, recorriendo los métodos con un bucle y mostrando su nombre
        static void Ejercicio6()
        {
            Console.WriteLine("\n--- Ejercicio 6 ---");
            Operar op = Suma;
            op += Resta;
            op += Multiplicacion;

            foreach (Operar metodo in op.GetInvocationList())
            {
                Console.WriteLine(metodo.Method.Name + ": " + metodo(5, 3));
            }
        }

        static int Suma(int a, int b) => a + b;
        static int Resta(int a, int b) => a - b;
        static int Multiplicacion(int a, int b) => a * b;

        // 7. Los métodos reciben un array: suma de todos los elementos y multiplicación de todos
        static void Ejercicio7()
        {
            Console.WriteLine("\n--- Ejercicio 7 ---");
            int[] numeros = { 2, 3, 4, 5 };

            OperarArray op = SumaArray;
            op += MultiplicarArray;

            foreach (OperarArray metodo in op.GetInvocationList())
            {
                Console.WriteLine(metodo.Method.Name + ": " + metodo(numeros));
            }
        }

        static int SumaArray(int[] numeros)
        {
            int suma = 0;
            foreach (int n in numeros)
            {
                suma += n;
            }
            return suma;
        }

        static int MultiplicarArray(int[] numeros)
        {
            int producto = 1;
            foreach (int n in numeros)
            {
                producto *= n;
            }
            return producto;
        }

        // 8. Delegado Transformar con Doblar, Triplicar y Cuadrado
        static void Ejercicio8()
        {
            Console.WriteLine("\n--- Ejercicio 8 ---");
            Console.WriteLine("Introduce un número:");
            int numero = Convert.ToInt32(Console.ReadLine());

            Transformar transformar = Doblar;
            transformar += Triplicar;
            transformar += Cuadrado;

            foreach (Transformar metodo in transformar.GetInvocationList())
            {
                Console.WriteLine(metodo.Method.Name + ": " + metodo(numero));
            }
        }

        static int Doblar(int x) => x * 2;
        static int Triplicar(int x) => x * 3;
        static int Cuadrado(int x) => x * x;

        // 9. Callback: EjecutarOperacion recibe el delegado y devuelve el resultado
        static void Ejercicio9()
        {
            Console.WriteLine("\n--- Ejercicio 9 ---");
            Console.WriteLine("Suma: " + EjecutarOperacion(Suma, 5, 3));
            Console.WriteLine("Resta: " + EjecutarOperacion(Resta, 5, 3));
            Console.WriteLine("Multiplicación: " + EjecutarOperacion(Multiplicar, 5, 3));
        }

        static int EjecutarOperacion(OperacionCallback op, int x, int y)
        {
            return op(x, y);
        }

        // 10. Delegado Calculo con raíz cuadrada y logaritmo, manejando las excepciones
        static void Ejercicio10()
        {
            Console.WriteLine("\n--- Ejercicio 10 ---");
            EjecutarCalculo(RaizCuadrada, 16);
            EjecutarCalculo(RaizCuadrada, -4);
            EjecutarCalculo(Logaritmo, 10);
            EjecutarCalculo(Logaritmo, 0);
        }

        static void EjecutarCalculo(Calculo calculo, double numero)
        {
            try
            {
                double resultado = calculo(numero);
                Console.WriteLine(calculo.Method.Name + "(" + numero + ") = " + resultado.ToString("F4"));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        static double RaizCuadrada(double x)
        {
            if (x < 0)
            {
                throw new ArgumentException("No se puede calcular la raíz cuadrada de un número negativo.");
            }
            return Math.Sqrt(x);
        }

        static double Logaritmo(double x)
        {
            if (x <= 0)
            {
                throw new ArgumentException("El logaritmo solo existe para números mayores que 0.");
            }
            return Math.Log(x);
        }
    }
}