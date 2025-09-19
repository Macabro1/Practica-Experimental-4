using System;
using System.Collections.Generic;
using System.Diagnostics;

class NodoArbol
{
    public int Valor;
    public NodoArbol Izquierdo, Derecho;

    public NodoArbol(int valor)
    {
        Valor = valor;
        Izquierdo = Derecho = null;
    }
}

class ArbolBinario
{
    public NodoArbol Raiz;

    public ArbolBinario() { Raiz = null; }

    public void Insertar(int valor)
    {
        Stopwatch sw = Stopwatch.StartNew();
        Raiz = InsertarRecursivo(Raiz, valor);
        sw.Stop();
        Console.WriteLine($"Insertado nodo {valor} en {sw.ElapsedTicks} ticks.");
    }

    private NodoArbol InsertarRecursivo(NodoArbol nodo, int valor)
    {
        if (nodo == null) return new NodoArbol(valor);

        if (valor < nodo.Valor)
            nodo.Izquierdo = InsertarRecursivo(nodo.Izquierdo, valor);
        else if (valor > nodo.Valor)
            nodo.Derecho = InsertarRecursivo(nodo.Derecho, valor);

        return nodo;
    }

    public void RecorridoInOrden(NodoArbol nodo)
    {
        if (nodo != null)
        {
            RecorridoInOrden(nodo.Izquierdo);
            Console.Write(nodo.Valor + " ");
            RecorridoInOrden(nodo.Derecho);
        }
    }

    public void RecorridoPreOrden(NodoArbol nodo)
    {
        if (nodo != null)
        {
            Console.Write(nodo.Valor + " ");
            RecorridoPreOrden(nodo.Izquierdo);
            RecorridoPreOrden(nodo.Derecho);
        }
    }

    public void RecorridoPostOrden(NodoArbol nodo)
    {
        if (nodo != null)
        {
            RecorridoPostOrden(nodo.Izquierdo);
            RecorridoPostOrden(nodo.Derecho);
            Console.Write(nodo.Valor + " ");
        }
    }

    public int ContarNodos(NodoArbol nodo)
    {
        if (nodo == null) return 0;
        return 1 + ContarNodos(nodo.Izquierdo) + ContarNodos(nodo.Derecho);
    }
}

class Grafo
{
    private Dictionary<string, List<string>> adyacencia;

    public Grafo()
    {
        adyacencia = new Dictionary<string, List<string>>();
    }

    public void AgregarVertice(string vertice)
    {
        if (!adyacencia.ContainsKey(vertice))
            adyacencia[vertice] = new List<string>();
    }

    public void AgregarArista(string origen, string destino)
    {
        Stopwatch sw = Stopwatch.StartNew();
        if (adyacencia.ContainsKey(origen) && adyacencia.ContainsKey(destino))
            adyacencia[origen].Add(destino);
        sw.Stop();
        Console.WriteLine($"Insertada arista {origen} → {destino} en {sw.ElapsedTicks} ticks.");
    }

    public void MostrarGrafo()
    {
        foreach (var vertice in adyacencia)
        {
            Console.WriteLine($"{vertice.Key} → {string.Join(", ", vertice.Value)}");
        }
    }

    public void ConsultarVertice(string vertice)
    {
        if (adyacencia.ContainsKey(vertice))
            Console.WriteLine($"{vertice} está conectado con: {string.Join(", ", adyacencia[vertice])}");
        else
            Console.WriteLine($"{vertice} no existe en el grafo.");
    }

    public int ContarVertices()
    {
        return adyacencia.Count;
    }

    public int ContarAristas()
    {
        int total = 0;
        foreach (var lista in adyacencia.Values)
            total += lista.Count;
        return total;
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Árbol Binario ===");
        ArbolBinario arbol = new ArbolBinario();
        arbol.Insertar(50);
        arbol.Insertar(30);
        arbol.Insertar(70);
        arbol.Insertar(20);
        arbol.Insertar(40);
        arbol.Insertar(60);
        arbol.Insertar(80);

        Console.WriteLine("\nRecorrido InOrden:");
        arbol.RecorridoInOrden(arbol.Raiz);
        Console.WriteLine("\nRecorrido PreOrden:");
        arbol.RecorridoPreOrden(arbol.Raiz);
        Console.WriteLine("\nRecorrido PostOrden:");
        arbol.RecorridoPostOrden(arbol.Raiz);

        Console.WriteLine($"\nTotal de nodos en el árbol: {arbol.ContarNodos(arbol.Raiz)}");

        Console.WriteLine("\n=== Grafo ===");
        Grafo grafo = new Grafo();
        grafo.AgregarVertice("Quito");
        grafo.AgregarVertice("Guayaquil");
        grafo.AgregarVertice("Cuenca");
        grafo.AgregarVertice("Ambato");

        grafo.AgregarArista("Quito", "Guayaquil");
        grafo.AgregarArista("Quito", "Cuenca");
        grafo.AgregarArista("Guayaquil", "Ambato");
        grafo.AgregarArista("Cuenca", "Quito");

        Console.WriteLine("\nLista de adyacencia del grafo:");
        grafo.MostrarGrafo();

        Console.WriteLine("\nConsulta de conexiones:");
        grafo.ConsultarVertice("Quito");
        grafo.ConsultarVertice("Cuenca");

        Console.WriteLine($"\nTotal de vértices en el grafo: {grafo.ContarVertices()}");
        Console.WriteLine($"Total de aristas en el grafo: {grafo.ContarAristas()}");
    }
}