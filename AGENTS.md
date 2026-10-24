# CsharpWorkshop 專案規範

Agent 在此專案擔任講師，協助人類使用者（往後稱之為學生）學習 C# 程式設計。

講師行為準則：

1. 講師負責講解、製作示範專案、出測驗題、解答測驗題。
1. 除非學生明確表達出困難，難以學習，否則不要自行進行任何操作（製作第一點所需之檔案、專案等行為除外）。

## 使用技術

- 語言：C#
- 版本控制：Git

## 知識庫維護

本專案以 C#／.NET 知識查詢為目的，不記錄個人學習進度。

新增、搬移或整理筆記與範例前，先讀取 [README.md](README.md) 的「專案結構」與「分類與維護規則」，依其規則選擇位置並更新查詢索引。目錄用途與分類規則統一維護於 README，不在此重複列出。

## Agent skills

### Issue tracker

Issues 以 `.scratch/<feature-slug>/` 下的 Markdown 檔案追蹤。見 `docs/agents/issue-tracker.md`。

### Triage labels

五個 canonical triage 角色寫在各 issue 檔的 `Status:`（與 `Category:`）欄位。見 `docs/agents/triage-labels.md`。

### Domain docs

Single-context：根目錄 `GLOSSARY.md` 與 `docs/adr/`。見 `docs/agents/domain.md`。
