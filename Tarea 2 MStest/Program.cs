using System;

namespace AuthTesting
{
    public enum SortDirection
    {
        Ascendente,
        Descendente
    }

    public interface ILista
    {
        void AddLast(int value);
        void AddFirst(int value);
        void InsertInOrder(int value);
        void Invert();
        void PrintList();
        int GetMiddle();
    }

    public class Node
    {
        public int Value;
        public Node Next;
        public Node Prev;

        public Node(int value)
        {
            Value = value;
            Next = null;
            Prev = null;
        }
    }

    public class DoublyLinkedList : ILista
    {
        public Node Head;
        public Node Tail;
        private Node Middle;
        private int Count;

        public DoublyLinkedList()
        {
            Head = null;
            Tail = null;
            Middle = null;
            Count = 0;
        }

        public void AddLast(int value)
        {
            Node newNode = new Node(value);
            if (Head == null)
            {
                Head = Tail = newNode;
                Middle = newNode;
            }
            else
            {
                Tail.Next = newNode;
                newNode.Prev = Tail;
                Tail = newNode;
                Count++;
                UpdateMiddle();
            }
        }

        public void AddFirst(int value)
        {
            Node newNode = new Node(value);
            if (Head == null)
            {
                Head = Tail = newNode;
                Middle = newNode;
            }
            else
            {
                newNode.Next = Head;
                Head.Prev = newNode;
                Head = newNode;
                Count++;
                UpdateMiddle();
            }
        }

        public void InsertInOrder(int value)
        {
            var newNode = new Node(value);

            if (Head == null)
            {
                Head = Tail = newNode;
                Middle = newNode;
            }
            else if (value <= Head.Value)
            {
                // Insertar al inicio
                newNode.Next = Head;
                Head.Prev = newNode;
                Head = newNode;
            }
            else if (value >= Tail.Value)
            {
                // Insertar al final
                newNode.Prev = Tail;
                Tail.Next = newNode;
                Tail = newNode;
            }
            else
            {
                // Insertar en el medio
                var current = Head;
                while (current != null && current.Value < value)
                {
                    current = current.Next;
                }
                newNode.Next = current;
                newNode.Prev = current.Prev;
                if (current.Prev != null)
                    current.Prev.Next = newNode;
                current.Prev = newNode;
            }

            Count++;
            UpdateMiddle();
        }

        private void UpdateMiddle()
        {
            if (Count == 0)
            {
                Middle = null;
            }
            else
            {
                var midIndex = Count / 2;
                var current = Head;
                for (int i = 0; i < midIndex; i++)
                {
                    current = current.Next;
                }
                Middle = current;
            }
        }

        public int GetMiddle()
        {
            if (Middle == null)
            {
                throw new InvalidOperationException("La lista está vacía.");
            }
            return Middle.Value;
        }

        public void PrintList()
        {
            Node current = Head;
            while (current != null)
            {
                Console.Write(current.Value + " ");
                current = current.Next;
            }
            Console.WriteLine();
        }

        public void Invert()
        {
            if (Head == null)
            {
                return;
            }

            Node current = Head;
            Node temp = null;

            while (current != null)
            {
                temp = current.Prev;
                current.Prev = current.Next;
                current.Next = temp;

                current = current.Prev;
            }

            if (temp != null)
            {
                Head = temp.Prev;
            }
        }
    }

    public class ListMerger
    {
        public static DoublyLinkedList MergeSorted(DoublyLinkedList listA, DoublyLinkedList listB, SortDirection direction)
        {
            DoublyLinkedList mergedList = new DoublyLinkedList();

            Node a = listA.Head;
            Node b = listB.Head;

            if (direction == SortDirection.Ascendente)
            {
                while (a != null && b != null)
                {
                    if (a.Value <= b.Value)
                    {
                        mergedList.AddLast(a.Value);
                        a = a.Next;
                    }
                    else
                    {
                        mergedList.AddLast(b.Value);
                        b = b.Next;
                    }
                }

                while (a != null)
                {
                    mergedList.AddLast(a.Value);
                    a = a.Next;
                }

                while (b != null)
                {
                    mergedList.AddLast(b.Value);
                    b = b.Next;
                }
            }
            else
            {
                while (a != null && b != null)
                {
                    if (a.Value <= b.Value)
                    {
                        mergedList.AddFirst(a.Value);
                        a = a.Next;
                    }
                    else
                    {
                        mergedList.AddFirst(b.Value);
                        b = b.Next;
                    }
                }

                while (a != null)
                {
                    mergedList.AddFirst(a.Value);
                    a = a.Next;
                }

                while (b != null)
                {
                    mergedList.AddFirst(b.Value);
                    b = b.Next;
                }
            }

            return mergedList;
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            DoublyLinkedList listA = new DoublyLinkedList();
            listA.AddLast(0);
            listA.AddLast(2);
            listA.AddLast(6);
            listA.AddLast(10);
            listA.AddLast(25);

            DoublyLinkedList listB = new DoublyLinkedList();
            listB.AddLast(3);
            listB.AddLast(7);
            listB.AddLast(11);
            listB.AddLast(25);
            listB.AddLast(25);
            listB.AddLast(40);
            listB.AddLast(50);

            Console.WriteLine("Lista A:");
            listA.PrintList();
            Console.WriteLine("Lista B:");
            listB.PrintList();

            DoublyLinkedList mergedAsc = ListMerger.MergeSorted(listA, listB, SortDirection.Ascendente);
            Console.WriteLine("Listas mezcladas Ascendente:");
            mergedAsc.PrintList();

            DoublyLinkedList mergedDesc = ListMerger.MergeSorted(listA, listB, SortDirection.Descendente);
            Console.WriteLine("Listas mezcladas Descendente:");
            mergedDesc.PrintList();

            DoublyLinkedList listInvert = new DoublyLinkedList();
            listInvert.AddLast(3);
            listInvert.AddLast(8);
            listInvert.AddLast(11);
            listInvert.AddLast(5);
            listInvert.AddLast(20);
            listInvert.AddLast(67);
            listInvert.AddLast(50);

            Console.WriteLine("Lista original:");
            listInvert.PrintList();

            listInvert.Invert();

            Console.WriteLine("Lista invertida:");
            listInvert.PrintList();

            DoublyLinkedList orderedList = new DoublyLinkedList();
            orderedList.InsertInOrder(20);
            orderedList.InsertInOrder(10);
            orderedList.InsertInOrder(15);
            orderedList.InsertInOrder(65);
            orderedList.InsertInOrder(10);

            Console.WriteLine("Lista ordenada:");
            orderedList.PrintList();

            Console.WriteLine("Elemento del medio de la lista:");
            Console.WriteLine(orderedList.GetMiddle());
        }
    }
}
