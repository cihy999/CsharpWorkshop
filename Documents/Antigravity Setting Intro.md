# Antigravity設定手冊

這份文件說明各項設定文件、檔案對 Antigravity 助手的影響。

## .gitignore

當 AI 分析專案時，`.gitignore` 會是一個過濾器，告訴 AI 哪些內容是雜訊，哪些才是核心。具體影響可以歸納為以下三點：

1. **聚焦核心邏輯**: 幫助 AI 過濾編譯產生的雜訊，專注於原始碼。
2. **安全性與穩定性**: 避免 AI 修改到 IDE 設定或自動生成檔案。
3. **執行效率**: 減少掃描不必要檔案的時間，加快回應速度。

## GEMINI.md

[官方文件](https://antigravity.google/docs/rules-workflows)

`GEMINI.md` 是 AI 助手的**全局自定義指令 (Global Custom Instructions)**檔案。

1. **持久化記憶 (Persistent Memory)**：它是存放您希望 AI 助手在所有對話中都遵循的規則的地方。
2. **跨工作區生效**：由於存放在用戶目錄下，無論您在哪個專案工作，這些規則都會被讀取。
3. **強制性執行**：助手在啟動每段對話前都會參考此檔案中的內容。

### 如何修改

1. 從對話-Customizations開啟，選擇+Global，即可編輯GEMINI.md。
2. 存檔後，新開啟的對話或後續的操作將會立即套用這些變更。

### 如何運用它？

根據個人習慣，隨時修改或新增指令。以下是一些建議的用途：

- **語言偏好**：固定回覆的語言（如繁體中文）。
- **程式碼風格**：例如「撰寫 C# 時請遵循 Clean Code 原則」或「使用特定的命名規範」。
- **回覆口吻**：例如「解釋請簡明扼要」或「回覆時請像資深工程師在進行 Code Review」。
- **特定格式**：例如「在回覆的最後附上相關的參考連結」。

### 設定範例

```text
"Always reply in Traditional Chinese (zh-TW)."
```

確保了無論使用什麼語言提問，助手都會自動以**繁體中文**進行回覆。

## .agent/rules

相較於全域的 `GEMINI.md`，這些規則**只會在當前專案中生效**。這讓您可以針對不同的專案需求（例如：A 專案用 C++，B 專案用 Python）設定專屬的開發準則。

### 核心概念

1. **專案專屬 (Project-Specific)**：規則檔案存放在專案根目錄的 `.agent/rules/` 資料夾中，不會影響其他專案。
2. **模組化管理 (Modular)**：不需要將所有規則塞在同一個檔案。您可以建立多個 `.md` 檔案（如 `csharp.md`, `git.md`, `naming.md`），AI 會自動讀取並整合。
3. **精準觸發 (Frontmatter Control)**：透過檔案開頭的設定區塊，可以指定規則何時生效（例如只在編輯 `.cs` 檔案時啟用）。

### 目錄結構範例

```text
MyProject/
├── .agent/
│   ├── rules/
│   │   ├── csharp.md      (C# 專用規則)
│   │   ├── database.md    (資料庫設計規範)
│   │   └── testing.md     (單元測試標準)
│   └── workflows/         (工作流程)
```

### 設定範例 (Frontmatter)

```text
---
description: C# 專案的開發規範  # 讓 AI 理解這份文件的用途
globs: ["**/*.cs"]             # 觸發條件：當涉及 .cs 檔案時自動啟用
---
```

### 實戰應用：C# 命名慣例 (csharp.md)

```text
# C# Coding Standards

1. **類別與方法**: 使用 `PascalCase` (大寫開頭)。
2. **變數**: 使用 `camelCase` (小寫開頭)。
3. **介面**: 以 `I` 開頭 (如 `ILogger`)。
```
