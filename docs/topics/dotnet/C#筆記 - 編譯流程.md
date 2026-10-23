---
title: C#筆記 - 編譯流程
tags: [C#]

---

# C# 編譯流程

```css
1. C# 原始碼 (.cs)
    ↓ 編譯 (C# Compiler：csc.exe 或 Roslyn)
2. 中繼語言 IL (.dll 或 .exe, 含 CIL + Metadata)
    ↓ 執行（由 OS 載入 .NET Runtime）
3. CLR 啟動
    ↓
4. JIT 編譯特定方法
    ↓
5. Native Code 執行
```

## 1. C# 編譯器 (csc.exe / Roslyn) 將 C# 編譯為 CIL

```css
.cs → Roslyn → IL → YourApp.dll
                   ↓
           包裝為 YourApp.exe（僅 Windows）
                   ↓
           加入 json（deps.json、runtimeconfig.json）
```

假設，對一個 C# Console App 執行 Visual Studio 的建置功能時，會產生以下幾類檔案：

`專案目錄/bin/Debug/net8.0/`

1. `ConsoleAttribute1.exe`：主執行檔（僅在 Windows 上為 .exe，Linux/macOS 通常無）。
2. `ConsoleAttribute1.dll`：主組件，包含 IL + Metadata（真正的程式本體）。
3. `ConsoleAttribute1.pdb`：除錯資訊檔案（可選）
4. `ConsoleAttribute1.deps.json`：說明此應用程式需要哪個 .NET 執行時版本
5. `ConsoleAttribute1.deps.json`：描述此應用程式需要哪些元件（含套件）

**IL(Intermediate Language)、CIL (Common Intermediate Language)、微軟中介語言 (Microsoft Intermediate Language, MSIL)**：

- IL 是一種**低階、平台無關**的語言
- 由 C# 編譯器產生（例如 Roslyn）
- **不是 CPU 原生碼**，而是要由 .NET Runtime 的 **JIT 編譯器** 在執行時轉換成原生碼
- 每一種 .NET 語言（C#, F#, VB.NET）都可以轉成 IL → 這是跨語言互通的關鍵

**Metadata**：描述類別、方法、屬性等結構資訊。

.NET 產生的 `.exe` 與 `.dll` 檔案是 **PE 格式** ，用來後續執行時的判斷。

## 2. 作業系統啟動應用程式 → 呼叫 .NET Runtime (CLR)

Windows 載入 PE 格式的 `.exe` 檔時，發現它是 .NET 組件（透過 CLR Header 判斷），就會呼叫 `mscoree.dll` 中的 `_CorExeMain` 函式。這個函式會根據組件的設定（例如 `runtimeconfig.json`）載入適當版本的 CLR，並交由 CLR 負責後續執行與管理。

## 3. CLR 初始化與安全檢查

1. 載入`ConsoleAttribute1.dll` (組件)
2. 解析 `.deps.json`，定還需要載入哪些其他 .dll（例如 NuGet 套件）
3. 檢查中繼資料(Metadata)（驗證安全性與正確性）
4. 檢查 IL 安全性，確保程式碼是型別安全的（Type Safe）
5. 準備 JIT 編譯，編譯器將 IL 轉換為特定 CPU 的原生語言

## 4. JIT 編譯 IL → Native Code

CLR 呼叫 **JIT 編譯器（Just-In-Time Compiler）**，JIT 把 IL（Intermediate Language）**轉換成對應平台的原生機器碼**。

這個編譯通常是**方法層級（Method-by-method）**，而不是整個程式一次轉換。

## 5. 執行 `Main()` 方法

JIT 編譯完成後，CLR 呼叫你專案中設定的 Entry Point：

```csharp
static void Main(string[] args)
```

## 6. 執行時期管理由 CLR 處理

| 功能 | 說明 |
| --- | --- |
| GC（垃圾回收） | 自動管理記憶體釋放 |
| 安全性機制 | 例如 Code Access Security（CAS） |
| 異常處理 | 統一例外機制（`try-catch`） |
| 多執行緒 | 管理執行緒的生命週期 |
| 程式域管理 | 隔離不同 Assembly 的執行空間 |

## 組件 Assembly

當建置一個 Console App 專案時，產生以下檔案：

![Assembly_202506211225](https://hackmd.io/_uploads/BJlt43QVel.png)

| 檔案名稱 | 類型 | 說明 |
| -------- | -------- | -------- |
| `ConsoleAttribute1.exe` | **Assembly** | 主要執行檔。這是帶有 `Main()` 進入點的 .NET Assembly，可直接執行。內含 IL、Manifest、Metadata。 |
| `ConsoleAttribute1.dll` | **Assembly** | **同樣也是完整 Assembly**。可供工具如 `dotnet` 呼叫：`dotnet ConsoleAttribute1.dll`。通常與 `.exe` 是同一組內容。 |
| `ConsoleAttribute1.pdb` | **Debug 資訊檔** | **Program Database 檔案**，包含對應的原始碼行數與變數名稱等資訊，供除錯器使用。不是 Assembly。                          |
| `ConsoleAttribute1.deps.json` | **相依性描述檔** | 描述此專案所依賴的所有 NuGet 套件與其他組件，執行時由 .NET Runtime 讀取以載入依賴。 |
| `ConsoleAttribute1.runtimeconfig.json` | **執行設定檔** | 指定此程式執行所需的 .NET Runtime（如 net8.0），由 CLR 使用。 |

說明：

- `ConsoleAttribute1.exe` 是主 Assembly，帶 Main()。
- `ConsoleAttribute1.dll` 是同一組裝內容的 Library 版本，通常可被其他程式參考（或給 CLI 工具使用）。
- 其他檔案都是為了支援執行或除錯（不是 Assembly 本身）。

Assembly（無論是 .dll 或 .exe）主要由三個部分組成：IL 程式碼、清單 (manifest) 和中繼資料 (metadata)。

| 元件           | 作用                      | 類比             |
| ------------ | ----------------------- | -------------- |
| **IL 程式碼**   | 真正要執行的程式邏輯（尚未轉為機器碼）     | 程式的「內容」        |
| **Manifest** | 組件的總覽與身份資訊              | 書的「封面與目錄」      |
| **Metadata** | 類型與成員的描述（供 CLR 與工具理解使用） | 書中「各章節標題與結構說明」 |

## IL 程式碼 (Intermediate Language Codes)

由 C# 編譯器產生的已編譯、與平台無關的程式碼（中介語言）。在應用程式執行時，由 JIT（Just-In-Time）編譯器轉換成特定平台的機器碼執行（CPU 原生語言）。

可以透過工具（如 ILDASM、dnSpy）查看 IL 指令，協助除錯或反組譯。

## 清單 (Manifest)

作為組件的「目錄」，清單提供了關於組件本身及其依賴項的重要資訊。

包含以下內容：

- 組件識別資訊：如名稱、版本號、文化（Culture）、強式名稱（Strong Name）等。
- 相依性資訊：此組件依賴哪些其他組件（含版本資訊），有助於組件綁定。
- 資源：嵌入的非程式資源（如影像、文字檔、多國語系資源等）。

重要性：

1. 在執行階段進行組件綁定（Assembly Binding）時扮演關鍵角色。
2. 支援 Side-by-Side 執行（可以載入同名但不同版本的組件）。

## 中繼資料 (Metadata)

包含應用程式中所有類型的詳細描述，例如類別、介面、結構、列舉和屬性。當建立自訂屬性時，其資訊也會儲存在此處。

CLR 大量利用中繼資料來執行各種任務，工具和偵錯器也依賴這些中繼資料。

CLR 使用它進行：

- 類型安全檢查
- 方法與成員呼叫
- 記憶體配置與物件建立
- 反射（Reflection）
- 記憶體佈局與序列化

工具如 Visual Studio、IntelliSense、Reflector 都會依靠這些資訊來解析程式碼結構。

## 參考資料

- [Master C# Attributes with C# Reflection Like a Senior Developer](https://youtu.be/_l64jwuEiho?si=1zSrfivgKcp5kbW7)
