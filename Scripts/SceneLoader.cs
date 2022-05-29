using UnityEngine.SceneManagement;

public class SceneLoader
{
    // 씬 전환이 필요한 스크립트마다 UnityEngine.SceneManagement; 이름공간을 선언하지 않고 사용하도록 함 
    // 정적 메소드로 선언(static : 별도의 캐싱이 필요 없이 사용할 수 있음)
    public static void LoadScene(string sceneName="")
    {
        // 현재 씬을 다시 로드할 때는 매개변수 없이 호출 
        if ( sceneName == "" )
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        // 원하는 씬을 로드할 때는 씬 이름을 매개변수에 작성 
        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
