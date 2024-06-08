namespace ConsoleApp31
{
    public static class Util
    {
        public static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            int x = 5, y = 10;
            Console.WriteLine($"x = {x}, y = {y}");
            Util.Swap(ref x, ref y);
            Console.WriteLine($"x = {y}, y = {x}");
            string a = "Hello", b = "World";
            Console.WriteLine($"a = {a}, b = {b}");
            Util.Swap(ref a, ref b);
            Console.WriteLine($"a = {a}, b = {b}");

            PriorityQueue<string> pq = new PriorityQueue<string>();
            pq.Enqueue("Low Priority Task", 3);
            pq.Enqueue("Medium Priority Task", 2);
            pq.Enqueue("High Priority Task", 1);
            Console.WriteLine($"Dequeue: {pq.Dequeue()}");
            Console.WriteLine($"Dequeue: {pq.Dequeue()}");
            Console.WriteLine($"Dequeue: {pq.Dequeue()}");

            CircularQueue<int> cq = new CircularQueue<int>(5);
            cq.Enqueue(1);
            cq.Enqueue(2);
            cq.Enqueue(3);
            cq.Enqueue(4);
            cq.Enqueue(5);
            Console.WriteLine($"Dequeue: {cq.Dequeue()}");
            Console.WriteLine($"Dequeue: {cq.Dequeue()}");
            Console.WriteLine($"Dequeue: {cq.Dequeue()}");

            SinglyLinkedList<int> list = new SinglyLinkedList<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            Console.WriteLine($"Contains 2: {list.Contains(2)}");  
            Console.WriteLine($"Remove 2: {list.Remove(2)}");      
            Console.WriteLine($"Contains 2: {list.Contains(2)}");  
            Console.WriteLine($"Count: {list.Count}");

            DoublyLinkedList<int> list2 = new DoublyLinkedList<int>();
            list2.Add(1);
            list2.Add(2);
            list2.Add(3);

            Console.WriteLine($"Contains 2: {list2.Contains(2)}");  
            Console.WriteLine($"Remove 2: {list2.Remove(2)}");      
            Console.WriteLine($"Contains 2: {list2.Contains(2)}");
            Console.WriteLine($"Count: {list2.Count}");
        }
    }
}
public class PriorityQueue<T>
{
    private SortedDictionary<int, Queue<T>> storage;
    public PriorityQueue()
    {
        storage = new SortedDictionary<int, Queue<T>>();
    }
    public void Enqueue(T item, int priority)
    {
        if (!storage.ContainsKey(priority))
        {
            storage[priority] = new Queue<T>();
        }
        storage[priority].Enqueue(item);
    }
    public T Dequeue()
    {
        if (storage.Count == 0)
            throw new InvalidOperationException("Queue is empty.");
        foreach (var pair in storage)
        {
            if (pair.Value.Count > 0)
            {
                T item = pair.Value.Dequeue();
                if (pair.Value.Count == 0)
                {
                    storage.Remove(pair.Key);
                }
                return item;
            }
        }
        throw new InvalidOperationException("Queue is empty.");
    }
    public T Peek()
    {
        if (storage.Count == 0)
            throw new InvalidOperationException("Queue is empty.");

        foreach (var pair in storage)
        {
            if (pair.Value.Count > 0)
            {
                return pair.Value.Peek();
            }
        }
        throw new InvalidOperationException("Queue is empty.");
    }
    public int Count
    {
        get
        {
            int count = 0;
            foreach (var pair in storage)
            {
                count += pair.Value.Count;
            }
            return count;
        }
    }
    public bool IsEmpty
    {
        get
        {
            return Count == 0;
        }
    }
}
public class CircularQueue<T>
{
    private T[] elements;
    private int front;
    private int rear;
    private int count;
    public CircularQueue(int capacity)
    {
        elements = new T[capacity];
        front = 0;
        rear = -1;
        count = 0;
    }
    public void Enqueue(T item)
    {
        if (count == elements.Length)
            throw new InvalidOperationException("Queue is full.");

        rear = (rear + 1) % elements.Length;
        elements[rear] = item;
        count++;
    }
    public T Dequeue()
    {
        if (IsEmpty)
            throw new InvalidOperationException("Queue is empty.");

        T item = elements[front];
        front = (front + 1) % elements.Length;
        count--;
        return item;
    }
    public T Peek()
    {
        if (IsEmpty)
            throw new InvalidOperationException("Queue is empty.");

        return elements[front];
    }
    public int Count
    {
        get { return count; }
    }
    public bool IsEmpty
    {
        get { return count == 0; }
    }
    public bool IsFull
    {
        get { return count == elements.Length; }
    }
}
public class SinglyLinkedList<T>
{
    private class Node
    {
        public T Data;
        public Node Next;

        public Node(T data)
        {
            Data = data;
            Next = null;
        }
    }
    private Node head;
    public void Add(T item)
    {
        Node newNode = new Node(item);
        if (head == null)
        {
            head = newNode;
        }
        else
        {
            Node current = head;
            while (current.Next != null)
            {
                current = current.Next;
            }
            current.Next = newNode;
        }
    }
    public bool Remove(T item)
    {
        if (head == null) return false;

        if (head.Data.Equals(item))
        {
            head = head.Next;
            return true;
        }
        Node current = head;
        while (current.Next != null && !current.Next.Data.Equals(item))
        {
            current = current.Next;
        }
        if (current.Next == null) return false;
        current.Next = current.Next.Next;
        return true;
    }
    public bool Contains(T item)
    {
        Node current = head;
        while (current != null)
        {
            if (current.Data.Equals(item)) return true;
            current = current.Next;
        }
        return false;
    }
    public int Count
    {
        get
        {
            int count = 0;
            Node current = head;
            while (current != null)
            {
                count++;
                current = current.Next;
            }
            return count;
        }
    }
}
public class DoublyLinkedList<T>
{
    private class Node
    {
        public T Data;
        public Node Next;
        public Node Prev;
        public Node(T data)
        {
            Data = data;
            Next = null;
            Prev = null;
        }
    }
    private Node head;
    private Node tail;
    public void Add(T item)
    {
        Node newNode = new Node(item);
        if (head == null)
        {
            head = tail = newNode;
        }
        else
        {
            tail.Next = newNode;
            newNode.Prev = tail;
            tail = newNode;
        }
    }
    public bool Remove(T item)
    {
        Node current = head;
        while (current != null)
        {
            if (current.Data.Equals(item))
            {
                if (current.Prev != null)
                {
                    current.Prev.Next = current.Next;
                }
                else
                {
                    head = current.Next;
                }

                if (current.Next != null)
                {
                    current.Next.Prev = current.Prev;
                }
                else
                {
                    tail = current.Prev;
                }

                return true;
            }
            current = current.Next;
        }
        return false;
    }
    public bool Contains(T item)
    {
        Node current = head;
        while (current != null)
        {
            if (current.Data.Equals(item)) return true;
            current = current.Next;
        }
        return false;
    }
    public int Count
    {
        get
        {
            int count = 0;
            Node current = head;
            while (current != null)
            {
                count++;
                current = current.Next;
            }
            return count;
        }
    }
}