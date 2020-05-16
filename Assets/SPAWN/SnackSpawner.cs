using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Globalization;
using Newtonsoft.Json.Converters;

public class SnackSpawner : MonoBehaviour
{


[SerializeField]
string VIDEO_ID;
////////////////////////////////
////////////////////////////////
////////////////////////////////

    public GameObject snackPrefab;
    
    public float apiCallDelay = 2.0f;
    private Vector2 screenBounds;
    
    /////API KEYS HERE///////
    static private List<string> apiKeys = new List<string>(){
        "AIzaSyAnu-BbCqgO9rUclcR-h5R_8rGPHiNagjk", // livedonut
        "AIzaSyAVx7hn92bMgVTy9ynE-K6Dz9_rbgTI4Qw", // nextGame
        "AIzaSyDZrMEAHssNQ25Ns3qM2gPATMa7eiYmtkQ", // endGame
        "AIzaSyA-JgaVs6k5b5dmpOeiwawHBxvgN40vC7c", // spiderMan
        "AIzaSyAebqxEwyNZeLH4z1ZwpKSulwPWa2P2sk8", // newWorld
        "AIzaSyAebqxEwyNZeLH4z1ZwpKSulwPWa2P2sk8", // myApp
        "AIzaSyBpjI7c201j2w1V4rrHz18pxiVJmHjcIpY", // blenderBottle
        "AIzaSyBw6dJRLixRJrpI3JcZ08DfFJQqf1AKc-Y", // ridingBike
        "AIzaSyBwqHF_NBgzCdc45wKi5PHNVkLpaczvGgc", // shopList
        "AIzaSyD-OEvv7d7tuIKckHbSyhxu9jWGzUERI9k", // bouncingBall
        "AIzaSyANqSpVgnemHICpiu_9FPExjZi9oFLy3bk", // chickenRabbit
        "AIzaSyAshvdApTAhTUGlF8G_1wMB6k8CP8ElobI" // finalProject
    };
    private int pointer = 0;
    private int length = apiKeys.Count;
    private int MAX_CHAT_COUNT = 1000;
    
    
    private int likeCount = 0;
    private OrderedHashSet<string> seenChats = new OrderedHashSet<string>();
    private int seenChatCount = 0;
    
////////////////////////////////
// START OF CODE
////////////////////////////////
    
    
    // Start is called before the first frame update
    void Start()
    {
        VIDEO_ID = PlayerPrefs.GetString("video_ID");
        //Debug.Log(VIDEO_ID);
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        
        likeCount = CallYouTubeForLikes();
        StartCoroutine(keepCallingYoutube());
    }

    // Update is called once per frame
    void Update()
    {   
        //DEBUG: Spawn snack with mouse click
        if (Input.GetMouseButtonDown(0))
            SpawnSnack();
    }
    
////////////////////////////////
// SPAWN A SNACK
////////////////////////////////
    void SpawnSnack(){
        GameObject snack = Instantiate(snackPrefab) as GameObject;
        var xCoord = Random.Range(-screenBounds.x, screenBounds.x);
        var yCoord = Random.Range(screenBounds.y * 1.0f, screenBounds.y * 1.2f);
        snack.transform.position = new Vector2(xCoord, yCoord);
    }
    
    
////////////////////////////////
// API CALL
////////////////////////////////
    
    int CallYouTubeForLikes(){
    
        string API_KEY = getKey();
        //Debug.Log(API_KEY);
        string URL = string.Format("https://www.googleapis.com/youtube/v3/videos?key={0}&part=statistics&id={1}", API_KEY, VIDEO_ID);
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string jsonResponse = reader.ReadToEnd();
        Root videoData = JsonConvert.DeserializeObject<Root>(jsonResponse);
        int thisLikeCount = videoData.Items[0].Statistics.LikeCount;
        
        return thisLikeCount;
    }

    LiveChatItem[] CallYouTubeForChats(){
    
        string API_KEY = getKey();
        //Debug.Log(API_KEY);
        string LIVE_CHAT_ID = "Cg0KC1h1YTNuOHVxaWpBKicKGFVDVnhoX1dGa1VCZjRfSG9pS3BUUmhNQRILWHVhM244dXFpakE";
        string URL = string.Format("https://www.googleapis.com/youtube/v3/liveChat/messages?key={0}&part=id&liveChatId={1}", API_KEY, LIVE_CHAT_ID);
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string jsonResponse = reader.ReadToEnd();
        LiveChatMessages liveChatMessages = JsonConvert.DeserializeObject<LiveChatMessages>(jsonResponse);
        return liveChatMessages.Items;
    }

    void addSnacksByLikes(int thisLikeCount){
    int snackssToAdd = thisLikeCount - likeCount;
    if(snackssToAdd > 0){
        //Add carrots
        for (int index = 1; index <= snackssToAdd; index++)
        {
            SpawnSnack();
        }
    }
    likeCount = thisLikeCount;
    }
    
    // Add snack for each new live chat
    void addSnacksByLivechat(LiveChatItem[] items) {
    
    // Iterate from backwards to find new chats
    for (int i = items.Length; i --> 0; )
    {
        if (!seenChats.Contains(items[i].Id)) {
            SpawnSnack();
            seenChats.Add(items[i].Id);
            if (seenChats.Count > MAX_CHAT_COUNT) {
                seenChats.RemoveAt(seenChats.Count-1);
            }
            seenChatCount += 1;

            GameObject.Find("LikesText").GetComponent<Text>().text = ""+seenChatCount;
        }
    }
    }

    
    IEnumerator keepCallingYoutube(){
        while(true){
            yield return new WaitForSeconds(apiCallDelay);
            addSnacksByLikes(CallYouTubeForLikes());
            addSnacksByLivechat(CallYouTubeForChats());
        }
    }
    
////////////////////////////////
// API KEY GENERATOR
////////////////////////////////
    string getKey(){
        pointer+=1;
        //If exceed end, go back to start
        if(pointer==length){
            pointer = 0;
        }
        return apiKeys[pointer];
    }
}

////////////////////////////////
// STUPID CLASSES FOR JSON
////////////////////////////////
public class Root
{
    public string Kind { get; set; }

    public string Etag { get; set; }

    public Item[] Items { get; set; }
}

public class Item
{
    public string Kind { get; set; }

    public string Etag { get; set; }

    public string Id { get; set; }

    public Statistics Statistics { get; set; }
}

public class Statistics
{
    
    public long ViewCount { get; set; }

    public int LikeCount { get; set; }

    public long DislikeCount { get; set; }

    public long FavoriteCount { get; set; }

    public long CommentCount { get; set; }
}

public class LiveChatMessages
{
    public string Kind { get; set; }

    public string Etag { get; set; }

    public string NextPageToken { get; set; }

    public long PollingIntervalMillis { get; set; }

    public PageInfo PageInfo { get; set; }

    public LiveChatItem[] Items { get; set; }
}

public class LiveChatItem
{
    public string Kind { get; set; }

    public string Etag { get; set; }

    public string Id { get; set; }
}

public partial class PageInfo
{
    public long TotalResults { get; set; }

    public long ResultsPerPage { get; set; }
}

public class OrderedHashSet<T> : KeyedCollection<T, T>
{
    protected override T GetKeyForItem(T item)
    {
        return item;
    }
}