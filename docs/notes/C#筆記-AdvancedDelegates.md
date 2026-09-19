---
version: "202606270950"
last_review: 2026-06-27
description: "筆記 C# Delegate 進階應用方式。"
---

# C#筆記 - Advanced Delegates

## Advanced C# Delegates like a Senior Developer: Real-World Examples in .NET!

以下是針對[Advanced C# Delegates like a Senior Developer: Real-World Examples in .NET!](https://youtu.be/LEcxrLb3OpY?si=izj-ohCPrxhHBr6B)內容所製作的精煉摘要：

---

### 第一部分：核心價值 (High-Level Summary)

這部影片旨在帶領開發者**超越 C# 委派（Delegates）的基礎語法，深入探討其在 .NET 平台底層的編譯機制與實務設計模式中的進階應用**。核心結論指出，委派不僅是 C# 的語法糖，更是實現事件驅動、異步處理、設計模式（如觀察者與策略模式）、LINQ 及函數式編程的**底層核心基石**。

---

### 第二部分：深度解析 (Deep Dive)

#### ⚙️ 底層編譯與機制

* 透過 JetBrains dotPeek 反編譯工具揭示，C# 中的委派在本質上是**編譯器自動生成的私有密封類別（private sealed class）** [[03:57](http://www.youtube.com/watch?v=LEcxrLb3OpY&t=237)]。
* 當定義多個相同簽章的匿名委派時，.NET 平台會**優化並複用同一個編譯器生成類別**，僅在內部建立不同的方法映射 [[05:30](http://www.youtube.com/watch?v=LEcxrLb3OpY&t=330)]。

#### 🔔 事件驅動編程 (EDP) 與 EAP 模式

* **事件驅動編程**的五大核心要素（事件、事件處理器、發行者、訂閱者、委派）中，**委派定義了事件處理器的簽章規格**，是 UI 框架（如 WinForms, WPF）點擊事件的底層核心 [[06:39](http://www.youtube.com/watch?v=LEcxrLb3OpY&t=399)]。
* **事件型異步模式 (EAP)**（如舊版 `BackgroundWorker` 與 `WebClient`）在 async/await 出現前被廣泛使用，其進度通知與完成回呼皆是**依賴委派機制來避免阻塞主執行緒** [[14:19](http://www.youtube.com/watch?v=LEcxrLb3OpY&t=859)], [[16:20](http://www.youtube.com/watch?v=LEcxrLb3OpY&t=980)]。

#### 🧩 觀察者與策略設計模式

* **👁️ 觀察者模式**：可利用委派字典（Dictionary of Delegates）替代傳統的介面設計，讓任何**符合委派簽章的方法都能直接註冊為訂閱者**，達成更彈性的低耦合架構 [[19:36](http://www.youtube.com/watch?v=LEcxrLb3OpY&t=1176)]。
* **🎯 策略模式**：將演算法（如過濾條件）以委派（`Predicate`）形式由外部傳入。這樣能**在不修改主邏輯（Context）的前提下，動態抽換核心業務邏輯** [[24:57](http://www.youtube.com/watch?v=LEcxrLb3OpY&t=1497)]。

#### 🚀 LINQ、並行與函數式編程

* **📊 LINQ 擴充方法**：諸如 `Where`、`Select` 及 `FirstOrDefault` 等方法，底層皆大量使用 `Func` 委派來接收 Lambda 表達式 [[27:54](http://www.youtube.com/watch?v=LEcxrLb3OpY&t=1674)]。
* **⚔️ 並行處理 (Concurrency)**：不論是 Legacy 執行緒（`ThreadStart`）、執行緒池（`WaitCallback`），或是現代的 TPL 任務（`Task.Run`），**多執行緒與異步編程的底層皆是由委派驅動** [[29:51](http://www.youtube.com/watch?v=LEcxrLb3OpY&t=1791)], [[31:05](http://www.youtube.com/watch?v=LEcxrLb3OpY&t=1865)]。
* **🧠 函數式編程 (FP)**：C# 透過委派將函數視為**一等公民（First-Class Citizens）**。藉由擴充方法與委派的結合，可撰寫出無隱藏狀態、高可讀性的**宣告式代碼（Declarative Code）** [[32:27](http://www.youtube.com/watch?v=LEcxrLb3OpY&t=1947)]。

---

### 第三部分：實踐建議 (Take Action)

**總結重點**：

1. 建議在架構設計時，善用 `Action`、`Func` 與 `Predicate` 等內建委派來**實現邏輯抽離與策略模式**，提高代碼的擴充性與可測試性。
2. 理解委派底層的類別生成機制，能幫助開發者在撰寫高性能 .NET 應用時，優化記憶體配置並避免不必要的閉包（Closure）開銷。

**適用對象**：

適合已掌握 C# 基礎，想進一步跨越到資深開發者（Senior Developer）階級、或是想深入理解 .NET 底層設計與軟體架構模式的中階工程師。

這段影片在 11:31 到 17:09 之間主要介紹了 **事件型非同步模式 (Event-based Asynchronous Pattern, EAP)** 及其在 .NET 中的應用與委派的關係。以下是摘要整理：

## 事件型非同步模式 (Event-base Asynchronous Pattern, EAP) 簡介

* **背景**：此模式於 .NET 2.0 引入，旨在解決 .NET 1.0 非同步程式模型（APM）的不足。
* **核心目標**：利用「事件」來通知非同步操作的完成，確保主執行緒（Main Thread）保持回應，避免介面凍結（Blocking）。
* **執行流程**：
  1. 用戶端呼叫一個方法來啟動非同步操作。
  2. 該方法立即回傳（不阻塞呼叫者）。
  3. 操作在背景執行緒或執行緒池（Thread Pool）中執行。
  4. 操作完成後（成功、失敗或取消），會觸發一個事件。
  5. 用戶端透過處理該事件來取得結果或處理錯誤。

### 範例──Windows Forms(`BackgroundWorker`)

模擬網路下載進度顯示：

從 Visual Studio 的 ToolBox（工具箱）將 `BackgroundWorker` 等元件拖曳到設計介面時，Visual Studio 會自動在 `Form1.Designer.cs` 中幫生成它的宣告與初始化程式碼（預設名稱為 `backgroundWorker1`）。

跟著以下步驟來完成：

---

#### 第一步：透過屬性視窗設定 BackgroundWorker

您不需要在程式碼中手動寫 `WorkerReportsProgress = true` 等設定。請在 Visual Studio 的設計畫面（Form1.cs [Design]）中：

1. 點擊畫面下方的 **`backgroundWorker1`** 元件。
2. 在右下角的 **Properties（屬性）視窗**中，找到以下兩個屬性並將它們改為 **`True`**：
   * **`WorkerReportsProgress`** $\rightarrow$ 設為 `True`（允許回報進度）
   * **`WorkerSupportsCancellation`** $\rightarrow$ 設為 `True`（支援中途取消）

---

#### 第二步：自動產生事件處理方法

一樣在右下角的屬性視窗中：

1. 點擊屬性視窗上方的 **「閃電」圖示**（Events，事件）。
2. 找到 **`DoWork`**，在它右邊的空白欄位**按兩下滑鼠左鍵**。VS 會自動在 `Form1.cs` 中產生 `backgroundWorker1_DoWork` 方法，並自動註冊事件。
3. 用同樣的方法，在 **`ProgressChanged`** 和 **`RunWorkerCompleted`** 右邊也各**按兩下滑鼠左鍵**。

完成後，VS 會在 `Form1.cs` 中產生三個空的事件方法。

---

#### 第三步：撰寫按鈕點擊事件

請回到 `Form1.cs`，現在只需要撰寫當點擊 `button1` 時的處理邏輯（請在 Form 設計畫面雙擊 `button1` 來產生點擊事件）：

```csharp
private void button1_Click(object sender, EventArgs e)
{
    if (backgroundWorker1.IsBusy)
    {
        // 避免使用者重複取消
        button1.Enabled = false;
        // 取消背景工作
        backgroundWorker1.CancelAsync();
    }
    else 
    {
        button1.Text = "Cancel";
        progressBar1.Value = 0;
        label1.Text = $"{progressBar1.Value}%";

        // 啟動背景工作
        backgroundWorker1.RunWorkerAsync();
    }
}
```

---

#### 第四步：填入剛剛自動產生的三個事件程式碼

Step 1. 背景工作（`DoWork`）

```csharp
private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
{
    BackgroundWorker? worker = sender as BackgroundWorker;
    if (worker == null) return;

    for (int i = 1; i <= 100; i++)
    {
        // 檢查是否取消
        if (worker.CancellationPending)
        {
            e.Cancel = true;
            return;
        }

        // 模擬下載延遲
        Thread.Sleep(100);

        // 回報進度
        worker.ReportProgress(i);
    }
}
```

Step 2. 進度更新（`ProgressChanged`）

```csharp
private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
{
    progressBar1.Value = e.ProgressPercentage;
    label1.Text = $"{e.ProgressPercentage}%";
}
```

Step 3. 工作結束（`RunWorkerCompleted`）

```csharp
private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
{
    button1.Enabled = true;
    button1.Text = "OK";

    if (e.Error != null)
    {
        MessageBox.Show($"下載過程發生錯誤：{e.Error.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
    else if (e.Cancelled)
    {
        progressBar1.Value = 0;
        label1.Text = "0%";
        MessageBox.Show("下載已被取消！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
    else
    {
        MessageBox.Show("下載完成！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}
```

---

簡單回顧一下這次練習中最核心的兩個 C# 觀念，這對未來開發會非常有幫助：

1. **事件與委派（Events & Delegates）的應用**：
   因為專案資料夾名稱是 `P03_AdvancedDelegates`，其實剛剛使用的 `backgroundWorker1.DoWork += ...` 語法，背後正是 C# 的**委派（Delegate）**在運作！
   * `DoWork` 事件背後使用的是 `DoWorkEventHandler` 委派。
   * `ProgressChanged` 使用的是 `ProgressChangedEventHandler` 委派。
   * `RunWorkerCompleted` 使用的是 `RunWorkerCompletedEventHandler` 委派。
   Visual Studio 幫拖曳產生程式碼時，其實就是在幫建立這些委派的實例，並將它們綁定到寫的方法上。

2. **跨執行緒安全（Thread Safety）**：
   學到了最重要的一點：**不能在背景執行緒（`DoWork`）直接修改 UI 的控制項**。透過 `ReportProgress` 方法與 `ProgressChanged` 事件的「接力」，程式安全地把資料從背景執行緒傳回 UI 執行緒。這是多執行緒開發中最經典的設計模式之一。
