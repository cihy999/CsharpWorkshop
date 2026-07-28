# C#筆記 - async/await

## 教學摘要

### 第一部分：核心價值 (High-Level Summary)

**《[Master C# async/await with Concurrency Like a Senior](https://youtu.be/_fPNcQrB1JA?si=eYRZJjvFu39Qmgpj)》**旨在剖析 **.NET / C# 並發編程（Concurrency）** 的核心概念與底層機制，解決開發者容易混淆多執行緒、平行處理與非同步編程的痛點。核心結論在於：**Async/Await** 本質上是透過 C# 編譯器產生的 **狀態機（State Machine）** 實現高響應性與非阻塞 I/O，而非單純建立新執行緒。

---

### 第二部分：深度解析 (Deep Dive)

🧠 **觀念建立：並發模型與技術分類**

- **並發 (Concurrency)** 是宏觀概念，指在同一時間段內處理多個任務 [[01:26](https://www.youtube.com/watch?v=_fPNcQrB1JA&t=86)]。
- **多執行緒** 與 **平行處理** 用於 **CPU 密集型 (CPU-bound)** 任務；平行處理利用多 CPU 核心實現真正的物理平行 [[03:59](https://www.youtube.com/watch?v=_fPNcQrB1JA&t=239)][[07:30](https://www.youtube.com/watch?v=_fPNcQrB1JA&t=450)]。
- **非同步編程** 用於 **I/O 密集型 (I/O-bound)** 任務（如資料庫、網路請求），避免阻塞主執行緒 [[10:35](https://www.youtube.com/watch?v=_fPNcQrB1JA&t=635)][[12:03](https://www.youtube.com/watch?v=_fPNcQrB1JA&t=723)]。

🛠️ **技術實作：從同步到 Async/Await 演化**

- **同步阻塞 (V1)**：任務依序排隊，長時間操作會直接卡死主執行緒 [[15:44](https://www.youtube.com/watch?v=_fPNcQrB1JA&t=944)]。
- **手動 Task 委派 (V2)**：透過 `Task` 與回調機制實現非阻塞，但程式碼複雜且維護困難 [[17:08](https://www.youtube.com/watch?v=_fPNcQrB1JA&t=1028)]。
- **Async/Await 語法糖 (V3)**：讓非同步程式碼保持同步程式碼的可讀性，大幅提升開發效率 [[19:39](https://www.youtube.com/watch?v=_fPNcQrB1JA&t=1179)]。

🔍 **底層反編譯：Async 狀態機運作機制**

- **狀態機轉譯**：編譯器會將 `async` 方法轉化為實作 `IAsyncStateMachine` 的類別，並搭配 **Builder 模式** 進行調度 [[27:05](https://www.youtube.com/watch?v=_fPNcQrB1JA&t=1625)][[28:32](https://www.youtube.com/watch?v=_fPNcQrB1JA&t=1712)]。
- **MoveNext 流程**：初始狀態為 `-1`；遇到 `await` 且任務未完成時，狀態轉為 `0` 並註冊 `AwaitUnsafeOnCompleted` 後釋放執行緒，待完成後觸發第二次 `MoveNext` 續行 [[31:08](https://www.youtube.com/watch?v=_fPNcQrB1JA&t=1868)][[34:23](https://www.youtube.com/watch?v=_fPNcQrB1JA&t=2063)]。
- **同步優化**：若任務已完成（如 `Task.FromResult`），系統不會進行執行緒切換，而是在原執行緒直接完成 [[39:51](https://www.youtube.com/watch?v=_fPNcQrB1JA&t=2391)]。

---

### 第三部分：實踐建議 (Take Action)

💡 **實操建議**

1. **精準區分任務類型**：CPU 密集型任務使用平行處理（如 `Parallel`）；I/O 密集型任務全面採用 `async`/`await`。
2. **避免語法誤用**：除了 UI/事件處理外，切勿使用 `async void`，並養成理解語法糖背後狀態機開銷的習慣。

---
