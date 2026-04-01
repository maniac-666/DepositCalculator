# DepositCalculator — Калькулятор доходности вклада

> **Практическая работа** | МДК.04.02 «Обеспечение качества функционирования компьютерных систем»  
> **Студент:** Е.А. Прасолова | **Группа:** 3ИСИП-423  
> **Преподаватель:** Аксёнова Т.Г.

---

## Описание проекта

WPF-приложение для расчёта дохода по банковскому вкладу. Поддерживает два режима:

| Тип | Формула | Описание |
|-----|---------|----------|
| **Простые проценты** | `S = P × (1 + r × t / 12)` | Начисляются на начальную сумму в конце срока |
| **Сложные проценты** | `S = P × (1 + r / 12)^t` | Ежемесячная капитализация: проценты прибавляются к сумме каждый месяц |

Где: `P` — начальная сумма, `r` — годовая ставка (доля), `t` — срок в месяцах.

---

## Структура репозитория

```
DepositCalculator/
├── DepositCalculator.sln                  # Solution-файл
├── DepositCalculator/                     # WPF-приложение (.NET Framework 4.7.2)
│   ├── DepositCalculator.csproj
│   ├── Properties/AssemblyInfo.cs
│   ├── App.xaml / App.xaml.cs
│   ├── MainWindow.xaml / MainWindow.xaml.cs   # Интерфейс
│   └── DepositCalc.cs                     # Бизнес-логика (расчёты)
├── DepositCalculator.Tests/               # Автоматизированные тесты (MSTest)
│   ├── DepositCalculator.Tests.csproj
│   ├── Properties/AssemblyInfo.cs
│   ├── packages.config
│   └── DepositCalcTests.cs                # 12 модульных тестов
├── TestCases_DepositCalculator.pdf        # Тестовые сценарии (заполненный шаблон)
└── README.md
```

---

## Запуск приложения

### Требования
- Windows 10/11
- .NET Framework 4.7.2
- Visual Studio 2022 (или 2019)

### Сборка и запуск
1. Открыть `DepositCalculator.sln` в Visual Studio
2. Восстановить NuGet-пакеты (ПКМ по решению → «Восстановить пакеты NuGet»)
3. Собрать решение (Ctrl+Shift+B)
4. Запустить (F5)

### Запуск тестов
1. Открыть **Обозреватель тестов** (Тест → Обозреватель тестов)
2. Нажать **Запустить все**

---

## Тестирование

### Тестовые сценарии (5 шт.)

| # | ID | Название | Приоритет | Статус |
|---|---|---|---|---|
| 1 | TC_FUNC_1 | Расчёт простых процентов с корректными данными | Высокий | ✅ Зачет |
| 2 | TC_FUNC_2 | Расчёт сложных процентов с корректными данными | Высокий | ✅ Зачет |
| 3 | TC_VAL_1  | Валидация: отрицательная сумма вклада | Средний | ✅ Зачет |
| 4 | TC_VAL_2  | Валидация: нулевая процентная ставка | Средний | ✅ Зачет |
| 5 | TC_VAL_3  | Валидация: нечисловой ввод в поле суммы | Средний | ✅ Зачет |

Подробные тестовые сценарии — см. файл `TestCases_DepositCalculator.pdf`.

### Автоматизированные тесты (MSTest) — 12 шт.

| Тест | Описание |
|------|----------|
| `CalculateSimple_ValidInput_ReturnsCorrectResult` | Простые %: 100000, 12%, 6 мес. → 106000 |
| `CalculateSimple_ZeroRate_ReturnsOriginalAmount` | Ставка 0% → доход 0 |
| `CalculateSimple_OneMonth_ReturnsCorrectResult` | 1 месяц: 200000, 6% → 201000 |
| `CalculateCompound_ValidInput_ReturnsCorrectResult` | Сложные %: 100000, 12%, 6 мес. → 106152.02 |
| `CalculateCompound_ZeroRate_ReturnsOriginalAmount` | Сложные при 0% → доход 0 |
| `CompoundInterest_GreaterOrEqual_SimpleInterest` | Сложные ≥ простых |
| `CalculateCompound_12Months_ReturnsCorrectResult` | 500000, 10%, 12 мес. → 552340.95 |
| `CalculateSimple_NegativePrincipal_ThrowsException` | Отриц. сумма → исключение |
| `CalculateSimple_ZeroPrincipal_ThrowsException` | Нулевая сумма → исключение |
| `CalculateCompound_NegativeRate_ThrowsException` | Отриц. ставка → исключение |
| `CalculateSimple_ZeroMonths_ThrowsException` | Нулевой срок → исключение |
| `CalculateCompound_NegativeMonths_ThrowsException` | Отриц. срок → исключение |

**Все 12 тестов проходят успешно.**

---

## Технологии

- **Язык:** C#
- **Фреймворк:** .NET Framework 4.7.2, WPF
- **Тестирование:** MSTest v3
- **IDE:** Visual Studio 2022
