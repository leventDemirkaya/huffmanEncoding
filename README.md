# 🌳 Huffman Encoding

> ✨ **Description:**  
> This project implements the Huffman Encoding algorithm in **C# .NET 6.0** using **memory-friendly** data types such as lists and arrays.  
> With **lossless compression**, your data takes up less space and transfers faster.

## 📑 Contents  
- [📜 About the Project](#about-the-project)  
- [⚡ Features](#features)  
- [🧩 Algorithm Overview](#algorithm-overview)  
- [⚙️ Installation and Usage](#installation-and-usage)  
- [📊 Sample Output](#sample-output)  
- [🛠 Technologies](#technologies)  
- [🤝 Contributing](#contributing)  
- [📄 License](#license)  
- [📬 Contact](#contact)

<a id="about-the-project"></a>
## 📜 About the Project  
Huffman Encoding is a **lossless compression algorithm** that encodes data based on **character frequencies**.

This project:  
- 🔍 Calculates character frequencies  
- 🌲 Builds combinations using Huffman tree logic  
- 🔢 Generates variable-length bit codes per character  
- 📉 Reports bit comparison and compression ratio before and after compression

<a id="features"></a>
## ⚡ Features  
✅ Detailed frequency analysis  
✅ Code generation based on merging steps  
✅ Bit-level compression length calculation  
✅ Size comparison before and after compression  
✅ Console output of processing times and code lists

<a id="algorithm-overview"></a>
## 🧩 Algorithm Overview  
1. Calculate the frequency of each character in the input string.  
2. Sort characters by frequency in ascending order.  
3. Merge the two lowest-frequency characters to build the Huffman tree.  
4. Assign unique bit sequences to each character by traversing the tree (0 = left, 1 = right).  
5. Encode the text using these bit sequences for compression.  
6. Calculate compression ratio and processing time.

<a id="installation-and-usage"></a>
## ⚙️ Installation and Usage  
1. 📥 Clone the repository:  
   ```bash
   git clone https://github.com/leventDemirkaya/huffmanEncoding.git
   cd huffmanEncoding
   
<a id ="sample-output"></a>
## 📊 Sample Output

### 📜 Input Text
babadedefaca

### 🔢 Character Frequencies and Huffman Codes

| Character | Frequency | Huffman Code|
|----------|---------|--------------|
| a        | 4       | 0            |
| b        | 2       | 101          |
| d        | 2       | 100          |
| e        | 2       | 111          |
| f        | 1       | 1101         |
| c        | 1       | 1100         |

- ⏱ Processing Time: 0.0001 seconds
  
- 📦 Before Compression: 104 bits
  
- 📦 After Compression: 68 bits
  
- 📉 Compression Ratio: 34% savings
  
- 📝 Compressed Data
1010010101001111001111101011000

<a id ="technologies"></a>
## 🛠 Technologies
- 💻 C#
- 🖥 .NET 6.0
- 📂 Console application (single entry point: Program.cs)

<a id ="contributing"></a>
## 🤝 Contributing
💡 Contributions are very welcome!

- 🐛 Report issues via the Issues page
- 🚀 Submit pull requests for improvements

<a id ="license"></a>  
## 📄 License
📝 This project is licensed under the MIT License. See the LICENSE file for details.

<a id ="contact"></a> 
## 📬 Contact
📧 leventdemirkaya@outlook.com
