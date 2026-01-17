---
trigger: always_on
---

---

description: C# 專案的開發規範與命名慣例
globs: ["**/*.cs"]
---

# C# Coding Standards

## 命名慣例 (Naming Conventions)

1. **類別與方法 (Class & Method)**:
   - 使用 `PascalCase` (大寫開頭)。
   - 例如: `class Student`, `void CalculateScore()`

2. **變數與參數 (Variable & Parameter)**:
   - 使用 `camelCase` (小寫開頭)。
   - 例如: `int studentId`, `string firstName`

3. **介面 (Interface)**:
   - 名稱必須以 `I` 開頭。
   - 例如: `interface ILogger`

## 語法偏好 (Syntax Preference)

- **變數宣告**: 當右側型別明確時 (例如有 `new` 關鍵字)，請優先使用 `var`。
  - Yes: `var students = new List<Student>();`
  - No: `List<Student> students = new List<Student>();`
