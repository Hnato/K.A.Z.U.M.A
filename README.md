# K.A.Z.U.M.A 🧠

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![.NET 11](https://img.shields.io/badge/.NET-11.0-blueviolet.svg)](https://dotnet.microsoft.com/download/dotnet/11.0)
[![AvaloniaUI](https://img.shields.io/badge/UI-Avalonia-red.svg)](https://avaloniaui.net/)
[![LLM: Gemma 3 12B](https://img.shields.io/badge/LLM-Gemma--3--12B-orange.svg)](https://huggingface.co/google/gemma-3-12b)

**K.A.Z.U.M.A** is a high-performance, modular, and privacy-focused AI System Orchestrator. It integrates state-of-the-art local LLMs with system-level automation, speech recognition, and neural text-to-speech, providing a seamless bridge between natural language and computer control.

---

## 🚀 Key Features

- **Brain**: Powered by **Google Gemma 3 12B** for advanced reasoning and instruction following.
- **Multilingual Support**: Real-time Polish ↔ English translation using **MarianMT**.
- **Speech Interaction**: 
  - **STT**: High-accuracy speech recognition via **Whisper/Faster Whisper**.
  - **TTS**: Natural-sounding offline neural voice synthesis using **Piper TTS**.
- **System Orchestration**: Full OS control (Mouse, Keyboard, GUI Automation) through a secure tool-calling pipeline.
- **Cross-Platform UI**: Beautiful, responsive, and animated interface built with **AvaloniaUI** and **MVVM**.
- **Privacy First**: Fully offline capable, keeping your data and commands local.

---

## 🛠 Tech Stack

- **Backend**: C# 15, .NET 11.0
- **UI Framework**: AvaloniaUI (Fluent Theme)
- **AI Integration**: REST API for LLM (OpenAI-compatible)
- **Microservices**: Python (Flask) for Translation and STT
- **Automation**: InputSimulatorStandard, FlaUI
- **Dependency Injection**: Microsoft.Extensions.DependencyInjection

---

## 📂 Project Structure

```text
KAZUMA/
├── KAZUMA.UI             # Avalonia Desktop Application
├── KAZUMA.Core           # Interfaces, Models, and DTOs
├── KAZUMA.Orchestrator   # Main reasoning and tool execution engine
├── KAZUMA.LLM            # Local LLM Client (Gemma 3)
├── KAZUMA.Translation    # MarianMT Client
├── KAZUMA.Speech         # Whisper STT Integration
├── KAZUMA.TTS            # Piper TTS Integration
├── KAZUMA.Tools          # OS Control Services (Mouse/Keyboard)
└── PythonServices/       # Microservices for AI Models (Python/Flask)
```

---

## 🔧 Installation & Setup

### Prerequisites
1. **.NET 11 SDK** installed.
2. **Ollama** or **LM Studio** running locally (port `1234`).
3. **Python 3.10+** for microservices.

### Step 1: Clone and Build
```bash
git clone https://github.com/youruser/KAZUMA.git
cd KAZUMA
dotnet build
```

### Step 2: Setup Python Services
```bash
cd PythonServices
pip install -r requirements.txt
python translator.py
python stt_service.py
```

### Step 3: Launch
Run the **KAZUMA.UI** project from your IDE or via terminal:
```bash
dotnet run --project KAZUMA.UI
```

---

## 🤖 Usage Guide

### Command System
K.A.Z.U.M.A uses a special tag system to perform actions. You can type or speak commands naturally, and the AI will translate them into executable tags:

- **Move Mouse**: `Move mouse to 500 300` -> AI generates `[MOVE_MOUSE 500 300]`
- **Click**: `Click the left button` -> AI generates `[CLICK_LEFT]`
- **Type Text**: `Type 'Hello World'` -> AI generates `[TYPE Hello World]`
- **Key Press**: `Press Enter` -> AI generates `[PRESS_KEY VK_RETURN]`

### Voice Control
Click the **"Mów"** button in the UI. K.A.Z.U.M.A will listen to your Polish command, translate it to English for reasoning, perform the action, and reply back in Polish via TTS.

---

## 📜 License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

## 🤝 Contributing
Contributions are welcome! Please open an issue or submit a pull request for any improvements or new features.

---
*Created with 🧠 by Hnato and ThomasWack*
