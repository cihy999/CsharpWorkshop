# Cursor 設定手冊

這份文件說明各項設定文件、檔案對 Cursor 助手的影響。

## .gitignore

當 AI 分析專案時，`.gitignore` 會是一個過濾器，告訴 AI 哪些內容是雜訊，哪些才是核心。具體影響可以歸納為以下三點：

1. **聚焦核心邏輯**: 幫助 AI 過濾編譯產生的雜訊，專注於原始碼。
2. **安全性與穩定性**: 避免 AI 修改到 IDE 設定或自動生成檔案。
3. **執行效率**: 減少掃描不必要檔案的時間，加快回應速度。

## .cursorignore 與 .cursorindexingignore

[官方文件](https://docs.cursor.com/context/ignore-files)

### .cursorignore 檔案

- 完全封鎖。AI 看不到、搜不到、也無法回答相關問題。
- 適用於：敏感資料 (API keys, 憑證)、機密文件。

### .cursorindexingignore 檔案

- AI 預設「搜不到」這些檔案，不會主動引用。
- 但如果您「手動開啟」或「明確 @ 提到」該檔案，AI 仍然可以讀取並回答。
- 適用於：大型自動生成檔、文件檔、或不想干擾全域搜尋的舊程式碼。

## 規則

[官方文件](https://cursor.com/zh-Hant/docs/context/rules)

### 專案規則

專案規則是 Cursor 提供的一種「結構化指令」機制，讓 AI Agent (Chat) 與編輯器 (Inline Edit) 能夠持續遵循專案專屬的規範。

**基本結構**：專案規則通常以 .mdc 檔案形式儲存在專案根目錄的 .cursor/rules/ 資料夾下。

每個檔案包含兩個部分：

1. Frontmatter (元數據設定)：位於檔案最上方，由 --- 包圍，用來控制規則的啟動時機。
   - description: 描述這條規則的用途（這對 AI 判斷「何時該套用」非常重要）。
   - globs: 適用範圍。例如 ["**/*.cs"] 代表所有 C# 檔案。
   - alwaysApply: 是否一律套用。若設為 true，只要符合 glob 模式，AI 就會主動讀取此規則。
2. Content (規則本文)：使用 Markdown 語法撰寫

**規則套用的類型（Rule Types）**：

| UI 顯示類型 | 檔案中的 Frontmatter 關鍵設定 | 實際的行為 (會有什麼變化？) |
| :--- | :--- | :--- |
| **Always Apply** | `alwaysApply: true` | **最強制**。無論你在寫什麼檔案，這條規則都會載入。 |
| **Apply to Specific Files** | `globs: ["**/*.cs"]` (且 `alwaysApply: false`) | **精準打擊**。只有在編輯符合 `globs` 的檔案時才啟用。 |
| **Apply Intelligently** | `description: "..."` (且 `alwaysApply: false`) | **智慧載入**。AI 會根據問題與「描述」的相關性來決定是否載入。 |
| **Apply Manually** | 以上皆否 (或沒觸發時) | **被動觸發**。平時不啟用，除非在對話中主動輸入 `@規則名稱`。 |

> AI 的「隱形成本」：
> 當設定為 Always Apply 時，這條規則會佔用 AI 每次對話的 Token (記憶體)。如果規則很多且都設為 Always Apply，AI 處理長代碼的能力會變弱。

最佳「規則套用」配置：

- **全域規範**（如：請用繁體中文回覆、代碼要簡潔）：使用 Always Apply。
- **語言規範**（如：C# 命名慣例、Python 縮排）：使用 Apply to Specific Files (搭配 globs)。這能確保 AI 在寫 Python 時不會被 C# 的規則干擾。
- **特定功能/套件**（如：如何使用特定的 API）：使用 Apply Intelligently。當你沒問到該功能時，AI 的大腦是清空的，效能最好。

最佳實務 (Best Practices)：

- **保持精簡**：單個規則檔案建議控制在 500 行以內，避免 AI 混淆。
- **具體且可執行**：避免「寫出乾淨的代碼」這種模糊說法，應改為「所有異步方法名稱必須以 Async 結尾」。
- **善用範例**：直接貼上正確的代碼片段，AI 模擬風格的能力會顯著提升。
- **路徑限定**：如果某個規則只針對 frontend/，就將 globs 設窄一點，節省 AI 的 Context 空間。
