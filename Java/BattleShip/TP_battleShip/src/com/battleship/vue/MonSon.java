package com.battleship.vue;

import java.io.*;
import javax.sound.sampled.*;
public class MonSon extends Thread
{
        String fichier;
       public MonSon()
        {

        }
        public void play(String fileName)
        {
                Thread t = new Thread(this);
                fichier = fileName;
                t.start();
        }
   
        public void run ()
        {
        	jouer(fichier);
        }
        private void jouer(String fileName)
        {
                File nomFichier = new File(fileName);
                AudioInputStream        audioInputStream = null;
                try
                {
                        audioInputStream = AudioSystem.getAudioInputStream(nomFichier);
                }
                catch (Exception e)
                {
                        e.printStackTrace();
                }
                AudioFormat     audioFormat = audioInputStream.getFormat();
                SourceDataLine  line = null;
                DataLine.Info   info = new DataLine.Info(SourceDataLine.class,audioFormat);
                try
                {
                        line = (SourceDataLine) AudioSystem.getLine(info);
                        line.open(audioFormat);
                }
                catch (LineUnavailableException e)
                {
                        e.printStackTrace();
                }
                catch (Exception e)
                {
                        e.printStackTrace();
                }
                line.start();
                int     nBytesRead = 0;
                byte[]  abData = new byte[128000];
                while (nBytesRead != -1)
                {
                        try
                        {
                                nBytesRead = audioInputStream.read(abData, 0, abData.length);
                        }
                        catch (IOException e)
                        {
                                e.printStackTrace();
                        }
                        if (nBytesRead >= 0)
                        {
                                int  nBytesWritten = line.write(abData, 0, nBytesRead);
                        }
                }
                line.drain();
                line.close();
        }
}