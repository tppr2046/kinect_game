using UnityEngine;
using System.IO;
using System;

public class GameDataLogger : MonoBehaviour
{
    // 儲存遊戲記錄的檔案名稱
    private const string FILE_NAME = "GameRecords.txt";
    // 完整的檔案路徑
    private string filePath;

    void Awake()
    {
        // 組合完整的檔案路徑
        filePath = Path.Combine(Application.persistentDataPath, FILE_NAME);

        Debug.Log("資料將儲存在: " + filePath);
    }

    /// <summary>
    /// 讀取檔案中目前所有的遊玩記錄，並計算遊玩次數。
    /// </summary>
    /// <returns>遊玩次數</returns>
    public int GetPlayCount()
    {
        if (!File.Exists(filePath))
        {
            return 0; // 檔案不存在，表示是第 0 次遊玩
        }

        try
        {
            // 讀取所有行並返回行數 (即遊玩次數)
            string[] lines = File.ReadAllLines(filePath);
            return lines.Length;
        }
        catch (Exception e)
        {
            Debug.LogError($"讀取檔案時發生錯誤: {e.Message}");
            return -1; // 發生錯誤
        }
    }

    /// <summary>
    /// 記錄一次遊玩的分數。
    /// </summary>
    /// <param name="score">本次遊戲獲得的分數</param>
    public void LogNewGame(int score)
    {
        // 格式化要寫入的字串 (例如: [2025/10/29 16:52:09] - Score: 1250)
        string logEntry = $"[{DateTime.Now:yyyy/MM/dd HH:mm:ss}] - Score: {score}";

        try
        {
            // 使用 StreamWriter 以附加模式 (append: true) 寫入新的一行
            // 這樣不會覆蓋掉舊的記錄
            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                sw.WriteLine(logEntry);
            }

            Debug.Log($"成功記錄新的遊戲分數: {logEntry}");
        }
        catch (Exception e)
        {
            Debug.LogError($"寫入檔案時發生錯誤: {e.Message}");
        }
    }
}