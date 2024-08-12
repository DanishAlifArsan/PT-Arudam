using System.Threading.Tasks;
using UnityEngine;

namespace YoutubePlayer
{
    public class PrepareYoutubeVideo : MonoBehaviour
    {
        public YoutubePlayer youtubePlayer;

        public async void Prepare()
        {
            try
            {
                Debug.Log("Loading video...");
                await youtubePlayer.PrepareVideoAsync();
                Debug.Log("Video ready");
            }
            catch (System.Exception)
            {
                Debug.Log("Error");
            }
            
        }
    }
}
