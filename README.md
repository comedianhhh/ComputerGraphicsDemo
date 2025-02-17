# Computer Graphics Demo 🎮✨  
*A MonoGame-based 3D graphics showcase featuring advanced shaders, post-processing effects, and interactive controls.*

[![MonoGame](https://img.shields.io/badge/MonoGame-3.8-%237A1C4C)](http://www.monogame.net/)
[![.NET](https://img.shields.io/badge/.NET-6.0-%23512BD4)](https://dotnet.microsoft.com/)
[![License](https://img.shields.io/badge/License-MIT-green)](LICENSE)

**Demo Preview**  
![Graphics Demo](media/demo.gif)

---

## 🌟 Key Features
- **Dynamic Shader System**
  - Real-time toggling of Diffuse/Specular/Normal mapping
  - Wireframe rendering mode
- **Post-Processing Effects**
  - Black & White conversion
  - Underwater distortion with wave simulation
  - Blue tint effect
- **Scene Management**
  - Space Fighter scene
  - Underwater scene
  - Custom skybox implementation
- **Interactive UI Controls**
  - Parameter adjustment (amplitude, frequency)
  - Scene switching
  - Effect toggling

---

## 🛠️ Technical Implementation

### Core Architecture
```csharp
// Form1.cs - UI Control Handlers
private void DiffuseCB_CheckedChanged(object sender, EventArgs e) {
    _game.SetShaderOption("Diffuse", DiffuseCB.Checked);
}

private void AmplitudeTB_Scroll(object sender, EventArgs e) {
    double amplitudeValue = (double)AmplitudeTB.Value / 100;
    _game.SetPPValue("Amplitude", amplitudeValue);
}
```
### Post-Processing Pipeline
```csharp
// Game1.cs - Post-Processing Implementation
if (enableUnderwater) {
    BlackAndWhiteShader.Parameters["frequency"].SetValue(frequency);
    BlackAndWhiteShader.Parameters["amplitude"].SetValue(amplitude);
    BlackAndWhiteShader.Parameters["time"].SetValue(time);
    
    _spriteBatch.Begin(SpriteSortMode.Immediate, effect: BlackAndWhiteShader);
    _spriteBatch.Draw(sceneRenderTarget, Vector2.Zero, Color.White);
    _spriteBatch.End();
}
```
### Shader System
|Shader File|	Purpose|
|---------|--------|
|FighterShader.fx|	Space fighter material shading|
|UnderWaterPost.fx|	Underwater distortion effects|
|BWPost.fx|	Black & white conversion|
|CubeMap.fx|	Skybox rendering|
## 🚀 Getting Started
Requirements
Visual Studio 2022+

MonoGame Pipeline Tool

.NET 6.0 SDK
