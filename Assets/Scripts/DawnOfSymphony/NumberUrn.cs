using System.Collections;
using System.Collections.Generic;
using System;

public class NumberUrn {

	bool m_autoReplenised = true;
	int m_numberOfOccurences;
	List<int> m_originList, m_shuffledList;
	Random m_randomGenerator;

	public int  SamplingCount {get {return  m_numberOfOccurences;} }
	public int SamplingLeft {get {return  m_shuffledList.Count;} }

	
	public NumberUrn(int numberOfOccurences){
		m_numberOfOccurences = numberOfOccurences;
		initUrnLists ();
	}

	public NumberUrn(int numberOfOccurences, bool autoReplenished){
		m_numberOfOccurences = numberOfOccurences;
		m_autoReplenised = autoReplenished;
		initUrnLists ();
	}


	private void initUrnLists(){
		m_randomGenerator = new Random ();
		m_originList = new List<int>();
		m_shuffledList = new List<int>();
		for (int i = 0; i < m_numberOfOccurences; i++) {m_originList.Add (i);}
		FillUrn ();
	}

	public void FillUrn(){
		m_shuffledList = m_originList.ConvertAll (i => i);
		m_shuffledList.Sort ((x, y) => m_randomGenerator.NextDouble() < 0.5f ? -1 : 1);
	}

	public int DraftValue(){

		int ret = -1;
		if (m_shuffledList.Count == 0 && !m_autoReplenised) 
			return -1;

		if (m_shuffledList.Count == 0 && m_autoReplenised)
			FillUrn ();


		ret = m_shuffledList [m_shuffledList.Count - 1];
		m_shuffledList.RemoveAt (m_shuffledList.Count - 1);

		return  ret;
	
	}
}
