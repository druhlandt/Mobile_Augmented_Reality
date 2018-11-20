using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Windows.Speech;

public class SpeechRecognizer : MonoBehaviour {

	KeywordRecognizer keywordRecognizer;
	Dictionary<string,System.Action> keywords = new Dictionary<string,System.Action>();
	public GameObject gameObj;
	private CountdownTimer cdt;


	void Start () {

		cdt= GetComponent<CountdownTimer>();
		
		keywords.Add ("Start", ()=>{
			cdt.run();
		});
		
		keywords.Add ("Pause", ()=>{
			cdt.run();
		});
		
		keywords.Add ("Plus", ()=>{
			cdt.raiseCurrentTime();
		});
		
		keywords.Add ("Minus", ()=>{
			cdt.degradeCurrentTime();
		});
		
		keywords.Add ("Fertig", ()=>{
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