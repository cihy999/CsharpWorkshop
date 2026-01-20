---
description: 確保討論計畫與本地存檔同步
---

# Planning Synchronization Rules

為了確保專案內的開發計畫始終保持最新，請遵循以下規則：

1. **模式守則**：執行此規劃流程時，應引導使用者使用 **Planning** 模式。
2. **初始存檔**：每當透過 `/planning` 工作流產生新的「實作計畫 (Implementation Plan)」時，必須同步寫入一份 Markdown 檔案至 `Shared/Plans/` 目錄。
3. **自動更新**：
   - 當使用者在對話中對已存在的計畫提出修改建議，且你更新了內部的計畫 Artifact 時。
   - 你必須**主動**同步更新對應在 `Shared/Plans/` 目錄下的 Markdown 檔案。
4. **檔案格式**：
   - 檔名規範：`{任務名稱簡稱}_{日期}.md`。
   - 檔案開頭必須包含 `# Implementation Plan: {任務名稱}`。
5. **透明性**：每次同步更新後，請在對話中告知使用者：「已同步更新實作計畫至 {檔案路徑}」。
