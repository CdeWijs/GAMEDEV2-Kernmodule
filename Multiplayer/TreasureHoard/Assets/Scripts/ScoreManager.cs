using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {
    private static string secretKey = "mySecretKey";
    public static string addScoreURL = "http://studenthome.hku.nl/~celine.dewijs/kernmodule/insert_score.php?";
    public string highscoreURL = "http://localhost/unity_test/display.php";
    public static string game = "TreasureHoard";

    void Start() {
        //StartCoroutine(GetScores());
    }
    
    public static IEnumerator PostScores(int score) {
        //This connects to a server side php script that will add the name and score to a MySQL DB.
        // Supply it with a string representing the players name and the players score.
        string hash = Md5Sum(game + score + secretKey);

        string url = addScoreURL + "SCORE=" + score + "&GAME=TreasureHoard" + "&HASH=" + hash;

        // Post the URL to the site and create a download object to get the result.
        using (WWW insert = new WWW(url)) {
            yield return insert; // Wait until the download is done
            if (insert.error != null) {
                print("There was an error posting the high score: " + insert.error);
            } else {
                Debug.Log(insert);
            }
        }

        // The above doesn't seem to work so this is temporary:
        //Application.OpenURL(url);
    }

    // Get the scores from the MySQL DB to display in a GUIText.
    // remember to use StartCoroutine when calling this function!
    //IEnumerator GetScores() {
    //    gameObject.guiText.text = "Loading Scores";
    //    WWW hs_get = new WWW(highscoreURL);
    //    yield return hs_get;

    //    if (hs_get.error != null) {
    //        print("There was an error getting the high score: " + hs_get.error);
    //    } else {
    //        gameObject.guiText.text = hs_get.text; // this is a GUIText that will display the scores in game.
    //    }
    //}

    public static string Md5Sum(string strToEncrypt) {
        System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
        byte[] bytes = ue.GetBytes(strToEncrypt);

        // encrypt bytes
        System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] hashBytes = md5.ComputeHash(bytes);

        // Convert the encrypted bytes back to a string (base 16)
        string hashString = "";

        for (int i = 0; i < hashBytes.Length; i++) {
            hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
        }

        return hashString.PadLeft(32, '0');
    }

}