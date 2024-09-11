using System;
using AuthTesting;


namespace MStest
{
    [TestClass]
    public class DoublyLinkedListTests
    {
        [TestMethod]
        public void AddFirst_agregrarnodoalinicio() //Se prueba que si solo hay un elemente head y tail deben ser el mismo
        {
            var list = new DoublyLinkedList();
            list.AddFirst(10);

            Assert.AreEqual(10, list.Head.Value);
            Assert.AreEqual(10, list.Tail.Value); 
        }

        [TestMethod]
        public void AddLast_agregrarnodoalfinal() //Se prueba que si solo hay un elemente head y tail deben ser el mismo
        {
            var list = new DoublyLinkedList();
            list.AddLast(10);

            Assert.AreEqual(10, list.Head.Value);
            Assert.AreEqual(10, list.Tail.Value); 
        }

        [TestMethod]
        public void AddFirst_agregarvariosnodos() // Se prueba que el primer agregado es la cola y cada elemento se agrega de primero
        {
            var list = new DoublyLinkedList();
            list.AddFirst(10);
            list.AddFirst(20);

            Assert.AreEqual(20, list.Head.Value);
            Assert.AreEqual(10, list.Tail.Value); 
        }

        [TestMethod]
        public void AddLast_agregarvariosnodos() // Se prueba que el primer agregado es la cabeza y cada elemento se agrega de último
        {
            var list = new DoublyLinkedList();
            list.AddLast(10);
            list.AddLast(20);

            Assert.AreEqual(10, list.Head.Value);
            Assert.AreEqual(20, list.Tail.Value); 
        }

        [TestMethod]
        public void PrintList_manejalistavacia()
        {
            var list = new DoublyLinkedList();

            using (var sw = new System.IO.StringWriter())
            {
                System.Console.SetOut(sw);
                list.PrintList();
                Assert.AreEqual(System.Environment.NewLine, sw.ToString());
            }
        }

        [TestMethod]
        public void PrintList_ordencorrecto()
        {
            var list = new DoublyLinkedList();
            list.AddLast(10);
            list.AddLast(20);
            list.AddLast(30);

            using (var sw = new System.IO.StringWriter())
            {
                System.Console.SetOut(sw);
                list.PrintList();
                Assert.AreEqual("10 20 30 " + System.Environment.NewLine, sw.ToString());
            }
        }

        [TestMethod]
        public void InsertInOrder_insertarunsoloelemento() //Probando que el método sirve si se agrega solo un elemento
        {
            var list = new DoublyLinkedList();
            list.InsertInOrder(10);

            Assert.AreEqual(10, list.Head.Value);
            Assert.AreEqual(10, list.Tail.Value);
            Assert.AreEqual(10, list.GetMiddle());
        }

        [TestMethod]
        public void InsertInOrder_insertarvarioselementos() //Probando que se pueden insertar varios elemento y se actualiza el nodo de la mitad
        {
            var list = new DoublyLinkedList();
            list.InsertInOrder(10);
            list.InsertInOrder(20);
            list.InsertInOrder(30);

            // Se espera: 10 20 30
            Assert.AreEqual(10, list.Head.Value);
            Assert.AreEqual(30, list.Tail.Value);
            Assert.AreEqual(20, list.GetMiddle()); // El del medio deberia ser 20
        }

        [TestMethod]
        public void InsertInOrder_insertandoenhead()
        {
            var list = new DoublyLinkedList();
            list.InsertInOrder(20);
            list.InsertInOrder(10); // Al ser un valor más pequeño debe insertarse correctamente en head

            // Se espera: 10 20
            Assert.AreEqual(10, list.Head.Value);
            Assert.AreEqual(20, list.Tail.Value);
            Assert.AreEqual(20, list.GetMiddle()); // El del medio debería ser 20 (se toma el de la derecha)
        }

        [TestMethod]
        public void InsertInOrder_insertandoenTail()
        {
            var list = new DoublyLinkedList();
            list.InsertInOrder(10);
            list.InsertInOrder(20); // Al ser un valor más grande debe insertarse correctamente en tail

            //Se espera: 10 20
            Assert.AreEqual(10, list.Head.Value);
            Assert.AreEqual(20, list.Tail.Value);
            Assert.AreEqual(20, list.GetMiddle()); // El del medio debería ser 20 (se toma el de la derecha)
        }

        [TestMethod]
        public void InsertInOrder_insertandoenlamitad()
        {
            var list = new DoublyLinkedList();
            list.InsertInOrder(10);
            list.InsertInOrder(30);
            list.InsertInOrder(20); //  Al ser un valor entre 10 y 30 debe insertarse correctamente en el medio

            // Se espera: 10  20  30
            Assert.AreEqual(10, list.Head.Value);
            Assert.AreEqual(30, list.Tail.Value);
            Assert.AreEqual(20, list.GetMiddle()); // El del medio deberia ser el 20
        }

        [TestMethod]
        public void InsertInOrder_insertarmaselementos() //Probando que funciona con más cantidad de elementos
        {
            var list = new DoublyLinkedList();
            list.InsertInOrder(10);
            list.InsertInOrder(20);
            list.InsertInOrder(30);
            list.InsertInOrder(40);
            list.InsertInOrder(50);
            // Se espera: 10 20 30 40 50
            Assert.AreEqual(10, list.Head.Value);
            Assert.AreEqual(50, list.Tail.Value);
            Assert.AreEqual(30, list.GetMiddle()); 
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetMiddle_listavacia() // Probando que logra manejar una lista vacía
        {
            var list = new DoublyLinkedList();
            list.GetMiddle(); 
        }
    }

    [TestClass]
    public class ListMergerTests
    {
        [TestMethod]
        public void MergeSorted_ascendente() // Probando que funciona correctamente en dirección ascendente
        {
            var listA = new DoublyLinkedList();
            listA.AddLast(1);
            listA.AddLast(3);

            var listB = new DoublyLinkedList();
            listB.AddLast(2);
            listB.AddLast(4);

            var mergedList = ListMerger.MergeSorted(listA, listB, SortDirection.Ascendente);

            int[] expected = { 1, 2, 3, 4 };
            Node current = mergedList.Head;
            foreach (var value in expected)
            {
                Assert.AreEqual(value, current.Value);
                current = current.Next;
            }
        }

        [TestMethod]
        public void MergeSorted_descendente() // Probando que funciona correctamente en dirección descendente
        {
            var listA = new DoublyLinkedList();
            listA.AddLast(1);
            listA.AddLast(3);

            var listB = new DoublyLinkedList();
            listB.AddLast(2);
            listB.AddLast(4);

            var mergedList = ListMerger.MergeSorted(listA, listB, SortDirection.Descendente);

            int[] expected = { 4, 3, 2, 1 };
            Node current = mergedList.Head;
            foreach (var value in expected)
            {
                Assert.AreEqual(value, current.Value);
                current = current.Next;
            }
        }

        [TestMethod]
        public void MergeSorted_listavacia() // Probando que logra manejar lista vacia
        {
            var listA = new DoublyLinkedList();
            var listB = new DoublyLinkedList();

            var mergedList = ListMerger.MergeSorted(listA, listB, SortDirection.Ascendente);

            Assert.IsNull(mergedList.Head);
        }

        [TestMethod]
        public void MergeSorted_vaciaascendente() //Probando que maneja una lista con elementos y otra vacia en dirección ascendente
        {
            var listA = new DoublyLinkedList();
            var listB = new DoublyLinkedList();
            listB.AddLast(10);
            listB.AddLast(20);

            var mergedList = ListMerger.MergeSorted(listA, listB, SortDirection.Ascendente);

            int[] expected = { 10, 20 };
            Node current = mergedList.Head;
            foreach (var value in expected)
            {
                Assert.AreEqual(value, current.Value);
                current = current.Next;
            }
        }

        [TestMethod]
        public void MergeSorted_vaciadescendente()  //Probando que maneja una lista con elementos y otra vacia en dirección descendente
        {
            var listA = new DoublyLinkedList();
            var listB = new DoublyLinkedList();
            listB.AddLast(10);
            listB.AddLast(20);

            var mergedList = ListMerger.MergeSorted(listA, listB, SortDirection.Descendente);

            int[] expected = { 20, 10 };
            Node current = mergedList.Head;
            foreach (var value in expected)
            {
                Assert.AreEqual(value, current.Value);
                current = current.Next;
            }
        }

        [TestMethod]
        public void MergeSorted_listasconunelemento() //Probando que maneja ambas listas con solo un elemento
        {
            var listA = new DoublyLinkedList();
            listA.AddLast(5);
            var listB = new DoublyLinkedList();
            listB.AddLast(10);

            var mergedList = ListMerger.MergeSorted(listA, listB, SortDirection.Ascendente);

            int[] expected = { 5, 10 };
            Node current = mergedList.Head;
            foreach (var value in expected)
            {
                Assert.AreEqual(value, current.Value);
                current = current.Next;
            }
        }

        [TestMethod]
        public void MergeSorted_elementosrepetidos() //Probando que maneja elementos repetidos
        {
            var listA = new DoublyLinkedList();
            listA.AddLast(5);
            listA.AddLast(10);
            listA.AddLast(10);

            var listB = new DoublyLinkedList();
            listB.AddLast(10);
            listB.AddLast(20);

            var mergedList = ListMerger.MergeSorted(listA, listB, SortDirection.Ascendente);

            int[] expected = { 5, 10, 10, 10, 20 };
            Node current = mergedList.Head;
            foreach (var value in expected)
            {
                Assert.AreEqual(value, current.Value);
                current = current.Next;
            }
        }
    }

    [TestClass]
    public class DoublyLinkedListInvertTests
    {
        [TestMethod]
        public void Invert_listavacia () //Probando que maneja lista vacia
        {
            var list = new DoublyLinkedList();
            list.Invert();

            Assert.IsNull(list.Head);
            Assert.IsNull(list.Tail);
        }

        [TestMethod]
        public void Invert_elementounico() //Probando que funciona con un solo elemento 
        {
            var list = new DoublyLinkedList();
            list.AddLast(1);
            list.Invert();

            Assert.AreEqual(1, list.Head.Value);
            Assert.AreEqual(1, list.Tail.Value);
            Assert.IsNull(list.Head.Next);
            Assert.IsNull(list.Head.Prev);
        }

        [TestMethod]
        public void Invert_varioselementos() //Probando que funciona con varios elementos
        {
            var list = new DoublyLinkedList();
            list.AddLast(1);
            list.AddLast(2);
            list.AddLast(3);
            list.AddLast(4);

            list.Invert();

            int[] expected = { 4, 3, 2, 1 };
            Node current = list.Head;
            foreach (var value in expected)
            {
                Assert.AreEqual(value, current.Value);
                current = current.Next;
            }
        }

        [TestMethod]
        public void Invert_elementosduplicados() //Probando que maneja elementos iguales
        {
            var list = new DoublyLinkedList();
            list.AddLast(1);
            list.AddLast(2);
            list.AddLast(2);
            list.AddLast(3);

            list.Invert();

            int[] expected = { 3, 2, 2, 1 };
            Node current = list.Head;
            foreach (var value in expected)
            {
                Assert.AreEqual(value, current.Value);
                current = current.Next;
            }
        }
    }
}
