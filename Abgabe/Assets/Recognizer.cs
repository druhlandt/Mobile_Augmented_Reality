using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Windows.Speech;

public class Recognizer : MonoBehaviour {

	KeywordRecognizer keywordRecognizer;
	Dictionary<string,System.Action> keywords = new Dictionary<string,System.Action>();
	public GameObject gameObj;
	private TestMethoden tm;
	private SoundTest st;
	private CountdownTimer cdt;


	void Start () {

		tm = GetComponent<TestMethoden>();
		st = GetComponent<SoundTest>();
		cdt= GetComponent<CountdownTimer>();

		keywords.Add ("Next", ()=>{
			st.PlayClip(st.nextSound);
			tm.NextCalled();
		});
		keywords.Add ("Back", ()=>{
			st.PlayClip(st.backSound);
			tm.BackCalled();
		});
		keywords.Add ("Sort", ()=>{
			st.PlayClip(st.sortSound);
			tm.SortCalled();
		});
		
		keywords.Add ("Start", ()=>{
			//st.PlayClip(st.sortSound);
			tm.StartCalled();
			cdt.run();
		});
		
		keywords.Add ("Pause", ()=>{
			//st.PlayClip(st.sortSound);
			tm.PauseCalled();
			cdt.run();
		});
		
		keywords.Add ("Plus", ()=>{
			//st.PlayClip(st.sortSound);
			tm.PlusCalled();
			cdt.raiseCurrentTime();
		});
		
		keywords.Add ("Minus", ()=>{
			//st.PlayClip(st.sortSound);
			tm.MinusCalled();
			cdt.degradeCurrentTime();
		});
		
		keywords.Add ("Fertig", ()=>{
			//st.PlayClip(st.sortSound);
			tm.FertigCalled();
			cdt.ready();
		});
		
		
		
		
		keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());
		keywordRecognizer.OnPhraseRecognized += KeywordReconizeOnPhraseReconized;
		keywordRecognizer.Start ();
	}

	void KeywordReconizeOnPhraseReconized(PhraseRecognizedEventArgs args){
		System.Action keywordAction;

		if(keywords.TryGetValue(args.text,out keywordAction)){
			keywordAction.Invoke();
		}
	}


	// Update is called once per frame
	void Update () {

	}
		
}