using System.Collections.Generic;
using System.Collections;
using System;
namespace test
{
	public class GenericArray<T> 
		: IEnumerable<T> 
	{
		public GenericArray()
			: this(default(T)) // domyślna wartość dla typu (np. int -> 0, dla klass null) 
		{
		}

		// Pozwala określić domyślną wartość dla elemntów pustych
		public GenericArray(T defaultValue)
		{
			_defaultValue = defaultValue;
		}
//Dictionary
		private readonly Dictionary<uint, T> _array;
		private readonly T _defaultValue;
		private uint _size;

		public GenericArray(uint size, T defaultValue)
		{
			_array = new Dictionary<uint, T>(); //Tworzenie tablicy konstruktorem
			this._size = size;
			_defaultValue = defaultValue;       //Określenie wartości elementów pustych
		}
		public T this[uint index]
		{
			get
			{
				return _array.ContainsKey(index) ? (T)_array[index] : _defaultValue; 
			}
			set 
			{		
				if (index >= _size) 
				{
					throw new IndexOutOfRangeException ();
				}
				// Sprawdza czy wartość jest różna od domyślnej i wstawia do tablicy
				if (!EqualityComparer<T>.Default.Equals(_defaultValue, value)) 
				{
					_array[index] = value;
					return;
				}
			}
		}
		public IEnumerator<T> GetEnumerator()
		{
			SortedDictionary<uint, T> sortKey = new SortedDictionary<uint,T>(_array);
			uint index = 0;
			uint help = 0;

			foreach (KeyValuePair<uint, T> item in sortKey)
			{
				while (index < item.Key)
				{
					index++;
					help = item.Key ;
					yield return _defaultValue; //Zwraca(dopiero gdy przejdzie po wszystkich elementach) wartości domyśle typu T (niezapisane)
				}
				index++;
				help = item.Key;
				yield return item.Value;        //Zwraca(dopiero gdy przejdzie po wszystkich elementach) wartości z tablicy generycznej (zapisane)
			}
		}
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
	class test
	{
		static void Main (string[] args)
		{
			GenericArray<int> x = new GenericArray<int> (2000000000, 0);
			for (uint i=0; i<1000; i++) 
			{
				x [i] = 1;
				Console.Write (x [i]);
			}
		}
	}
} 
//----------------------------------------------------------------





