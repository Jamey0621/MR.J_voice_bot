using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.Diagnostics;

namespace voiceBot
{
    public partial class Form1 : Form
    {
        // inti it
        SpeechSynthesizer s = new SpeechSynthesizer();
        //seting up the speech commands
        Choices list = new Choices();
        // name is list for the commands

        bool wake = true;
        public Form1()
        {
            // int speech rec
            SpeechRecognitionEngine rec = new SpeechRecognitionEngine();

               // will add these to a text file
            list.Add(new string[]{"hi how are you", "tell me about your self","what time is it","what is today","i need to look up something","open youtube","i need to do some code wars",
            "sleep","j","open spotify","open windows media player", "play", "pause","next song"
            });

            Grammar gr = new Grammar(new GrammarBuilder(list));


            try
            {
                rec.RequestRecognizerUpdate();
                rec.LoadGrammarAsync(gr);
                rec.SpeechRecognized += Rec_SpeechRecognized;
                rec.SetInputToDefaultAudioDevice();
                rec.RecognizeAsync(RecognizeMode.Multiple);
            }
            catch { return; }
          



            // testing to make sure it works
           // s.Speak("Test");
             // setting the gender of my vocie
            s.SelectVoiceByHints(VoiceGender.Neutral);
            s.SpeakAsync("Hello");
            s.SpeakAsync("I am Mr. J.");
           
           
            InitializeComponent();
        }

        


        public void say( string com)
        {
            s.Speak(com);
        }
        private void Rec_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string r = e.Result.Text;
            // wake and sleep
            if(r == "j")
            {
                say("What can i do for yo?");
                wake = true;
            }
            else if(r == "sleep")
            {
                say("If you need me say hey J");
                wake = false;
            }


            if (wake == true)
            {
                // command
                if (r == "hi how are you")
                {
                    // respond
                    say("I am great, how are you?");
                }
                if (r == "tell me about your self")
                {
                    say("I am a bot made by Jamey");
                }
                if (r == "what time is it")
                {
                    say(DateTime.Now.ToString("h mm tt "));
                }
                if (r == "what is today")
                {
                    say(DateTime.Now.ToString("M/d/yyyy "));
                }
               
                if (r == "i need to look up something")
                {
                    say("i will open up google for you");
                    Process.Start("http://google.com");
                }
                if (r == "open youtube")
                {
                    Process.Start("http://youtube.com");
                }
                if (r == "i need to do some code wars")
                {
                    say("get to work");
                    Process.Start("http://www.codewars.com");
                }
                if( r == "open spotify")
                {
                    Process.Start(@"Spotify.exe");
                }
                if(r == "play" || r == "pause")
                {
                    SendKeys.Send(" ");
                }
                if( r == "next song")
                {
                    SendKeys.Send("^{RIGHT}^");
                }
                    
                    
                if(r == "open windows media player")
                {
                    Process.Start(@"wmplayer.exe");
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
