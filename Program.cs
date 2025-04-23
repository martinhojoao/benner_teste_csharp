class Program
{
	static void Main()
	{
		Network network = new Network(8);
		network.Connect(1, 6);
		network.Connect(1, 2);
		network.Connect(2, 4);
		network.Connect(2, 6);
		network.Connect(5, 8);
		
		Console.WriteLine(network.Query(1, 2));
		Console.WriteLine(network.Query(1, 4));
		Console.WriteLine(network.Query(2, 7));
		Console.WriteLine(network.Query(5, 3));
		
		Console.WriteLine(network.LevelConnection(1, 2));
		Console.WriteLine(network.LevelConnection(1, 4));
		Console.WriteLine(network.LevelConnection(2, 7));
		Console.WriteLine(network.LevelConnection(5, 3));
	}
}

class Network
{
	
	protected int numberOfElements;
	
	protected Dictionary<int, List<int>> dictionary;
	
	private HashSet<int> visited;
	
	private int level;
	
	public Network(int numberOfElements) 
	{
		if (numberOfElements < 1)
		{
			throw new Exception("ERROR: the number of elements must be positive.");
		}
		
		this.numberOfElements = numberOfElements;
		this.dictionary = new Dictionary<int, List<int>>();
		this.visited = new HashSet<int>();
		
		for (int i = 1; i <= numberOfElements; i++)
		{
			dictionary.Add(i, new List<int>());
		}
	
	}
	
	public void Connect(int element01, int element02)
	{
		if (element01 > numberOfElements || element02 > numberOfElements || element01 < 0 || element02 < 0)
		{
			throw new Exception("ERROR: both elements must be within the valid range.");
		}
        
		if (dictionary.ContainsKey(element01) && dictionary.ContainsKey(element02))
		{
			if (!dictionary[element01].Contains(element02))
			{					
				dictionary[element01].Add(element02);
				dictionary[element02].Add(element01);	
			}
			else 
			{
				throw new Exception("ERROR: those elements are already connected.");
			}
		}
		else 
		{
			throw new Exception("ERROR: at least one of the provided numbers does not exist in the network.");
		}
	}
	
	public void Disconnect(int element01, int element02)
	{
		if (element01 >= numberOfElements || element02 >= numberOfElements || element01 < 0 || element02 < 0)
        {
            throw new Exception("ERROR: both elements must be within the valid range.");
        }
        
		if (dictionary.ContainsKey(element01) && dictionary.ContainsKey(element02))
		{
			if (dictionary[element01].Contains(element02))
			{					
				dictionary[element01].Remove(element02);
				dictionary[element02].Remove(element01);	
			}
			else 
			{
				throw new Exception("ERROR: those elements are not connected.");
			}
		}
		else 
		{
			throw new Exception("ERROR: at least one of the provided numbers does not exist in the network.");
		}
	}
	
	public bool Query(int element01, int element02)
	{
		visited.Clear();
		return QueryRecursive(element01, element02);
	}
	
	private bool QueryRecursive(int element01, int element02)
	{
		
		if (element01 > numberOfElements || element02 > numberOfElements || element01 < 0 || element02 < 0)
		{
		    	throw new Exception("ERROR: both elements must be within the valid range.");
		}
        
		if (visited.Contains(element01))
		{
			return false;
		}
		
		visited.Add(element01);
        
		if (dictionary[element01].Contains(element02))
		{
			return true;
		}
		
		foreach (int neighbor in dictionary[element01])
		{
			if (QueryRecursive(neighbor, element02)) 
			{
				return true;
			}
		}
		
		return false;	
	}
	
	public int LevelConnection(int element01, int element02)
	{
		bool areConnected = Query(element01, element02);
		visited.Clear();
		level = 0;
		return areConnected ? LevelConnectionRecursive(element01, element02) : 0;
	}
	
	private int LevelConnectionRecursive(int element01, int element02)
	{
		if (element01 > numberOfElements || element02 > numberOfElements || element01 < 0 || element02 < 0)
		{
		    throw new Exception("ERROR: both elements must be within the valid range.");
		}
        
		visited.Add(element01);
		
		if (dictionary[element01].Contains(element02))
		{
			return 1;
		}	
		
		level++;
		
		foreach (int neighbor in dictionary[element01]) 
		{
			if (!visited.Contains(neighbor))
			{
				LevelConnectionRecursive(neighbor, element02);
			}
		}
		
		return level;
	}
}

