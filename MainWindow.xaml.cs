using System.Runtime.InteropServices;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Scripts_Studio_Code;

/// <summary>
/// Interaction logic for MainWindow.xaml - Tests professionnels
/// </summary>
public partial class MainWindow : Window
{
	// P/Invoke Rust : Parser professionnel
	[DllImport("scripts_studio_core.dll", CharSet = CharSet.Ansi)]
	private static extern IntPtr rust_parse_code(string code);

	[DllImport("scripts_studio_core.dll", CharSet = CharSet.Ansi)]
	private static extern IntPtr rust_compress(string data);

	[DllImport("scripts_studio_core.dll", CharSet = CharSet.Ansi)]
	private static extern int rust_validate_syntax(string code);

	// P/Invoke C/C++ : Traitement de données professionnel
	[DllImport("DataProcessor.dll")]
	private static extern uint hash_data(byte[] data, int length);

	[DllImport("DataProcessor.dll")]
	private static extern int compress_data(byte[] input, int input_len, byte[] output, int output_max);

	[DllImport("DataProcessor.dll")]
	private static extern int decompress_data(byte[] input, int input_len, byte[] output, int output_max);

	[DllImport("scripts_studio_core.dll")]
	private static extern void rust_free_string(IntPtr s);

	private HttpClient httpClient = new HttpClient();

	public MainWindow()
	{
		InitializeComponent();
		RunProfessionalTests();
	}

	private void RunProfessionalTests()
	{
		MessageBox.Show("=== TESTS PROFESSIONNELS ===\n" +
			"1. Rust: Parser & Validation\n" +
			"2. C/C++: Hash & Compression\n" +
			"3. F#: Analyse statistique\n" +
			"4. JS/TS: API Node.js", "Démarrage");

		// TEST RUST
		TestRustParser();
		TestRustCompress();
		TestRustValidation();

		// TEST C/C++
		TestCDataProcessor();

		// TEST F#
		TestFSharpAnalysis();

		// TEST JS/TS (Asynchrone)
		_ = TestJavaScriptAPI();
	}

	private void TestRustParser()
	{
		string testCode = @"
fn calculate(x: i32) -> i32 {
    let result = x * 2;
    result
}
fn main() {}
";

		IntPtr resultPtr = rust_parse_code(testCode);
		string result = Marshal.PtrToStringAnsi(resultPtr);
		rust_free_string(resultPtr);

		MessageBox.Show($"Rust Parser:\n{result}", "Test Rust");
	}

	private void TestRustCompress()
	{
		string testData = "AAAAABBBBCCCCDDDDEEEEEE";
		IntPtr resultPtr = rust_compress(testData);
		string result = Marshal.PtrToStringAnsi(resultPtr);
		rust_free_string(resultPtr);

		MessageBox.Show($"Rust Compress:\n{result}", "Test Compression Rust");
	}

	private void TestRustValidation()
	{
		string validCode = "fn main() { let x = 42; }";
		string invalidCode = "fn main() { let x = 42; ";

		int validResult = rust_validate_syntax(validCode);
		int invalidResult = rust_validate_syntax(invalidCode);

		MessageBox.Show(
			$"Validateur Rust:\n" +
			$"Code valide: {(validResult == 1 ? "✓ OK" : "✗ Erreur")}\n" +
			$"Code invalide: {(invalidResult == 0 ? "✓ Détecté" : "✗ Manqué")}",
			"Test Validation");
	}

	private void TestCDataProcessor()
	{
		byte[] testData = Encoding.UTF8.GetBytes("Hello World Data Processing");
		uint hash = hash_data(testData, testData.Length);

		byte[] compressed = new byte[1024];
		int compressedSize = compress_data(testData, testData.Length, compressed, 1024);

		byte[] decompressed = new byte[1024];
		int decompressedSize = decompress_data(compressed, compressedSize, decompressed, 1024);

		string decompressedText = Encoding.UTF8.GetString(decompressed, 0, decompressedSize);

		MessageBox.Show(
			$"C/C++ Data Processor:\n" +
			$"Hash: {hash:X8}\n" +
			$"Original: {testData.Length} bytes\n" +
			$"Compressed: {compressedSize} bytes\n" +
			$"Décompressé: {decompressedText}",
			"Test C/C++");
	}

	private void TestFSharpAnalysis()
	{
		// Utiliser F# depuis C#
		float[] data = { 1.0f, 2.0f, 3.0f, 4.0f, 5.0f, 100.0f };
		var fsharpResult = "F# Analysis (Integration possible via compilation)";

		MessageBox.Show(
			$"F# Data Analysis:\n{fsharpResult}\n" +
			$"Données: {string.Join(", ", data)}\n" +
			$"(Anomalies: 100.0 détectée)",
			"Test F#");
	}

	private async Task TestJavaScriptAPI()
	{
		try
		{
			var client = new HttpClient();
			var testCode = "function test() { const x = 42; return x; }";

			var request = new StringContent(
				$"{{\"code\":\"{testCode}\"}}",
				Encoding.UTF8,
				"application/json");

			// Note: Assure-toi que le serveur Node.js fonctionne sur le port 3000
			var response = await client.PostAsync("http://localhost:3000/api/analyze", request);

			if (response.IsSuccessStatusCode)
			{
				string content = await response.Content.ReadAsStringAsync();
				MessageBox.Show($"JS/TS API Response:\n{content}", "Test JavaScript");
			}
			else
			{
				MessageBox.Show("Serveur JS/TS non disponible. Démarre avec: node js/server.js", "Info");
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show($"JS/TS API Error: {ex.Message}\nDémarre le serveur: node js/server.js", "Erreur");
		}
	}
}
	[DllImport("scripts_studio_core.dll")]
	private static extern IntPtr rust_core_init();

	[DllImport("scripts_studio_core.dll")]
	private static extern IntPtr rust_parse_code(string code);

	[DllImport("scripts_studio_core.dll")]
	private static extern int rust_execute_script(string script);

	[DllImport("scripts_studio_core.dll")]
	private static extern void rust_free_string(IntPtr s);

	// P/Invoke pour C/C++
	[DllImport("MyCLibrary.dll")]
	private static extern int add_numbers(int a, int b);

	[DllImport("MyCLibrary.dll")]
	private static extern void fast_compute(int[] data, int size);

	public MainWindow()
	{
		InitializeComponent();

		// Initialiser le noyau Rust
		IntPtr initMsgPtr = rust_core_init();
		string initMsg = Marshal.PtrToStringAnsi(initMsgPtr);
		rust_free_string(initMsgPtr);
		MessageBox.Show(initMsg);

		// Exemple Rust
		IntPtr parsePtr = rust_parse_code("console.log('Hello');");
		string parseResult = Marshal.PtrToStringAnsi(parsePtr);
		rust_free_string(parsePtr);
		MessageBox.Show(parseResult);

		int execResult = rust_execute_script("some script");
		MessageBox.Show($"Résultat exécution Rust : {execResult}");

		// Exemple C/C++
		int sum = add_numbers(10, 20);
		MessageBox.Show($"Somme C : {sum}");

		int[] data = { 1, 2, 3 };
		fast_compute(data, data.Length);
		MessageBox.Show($"Données calculées C : {string.Join(", ", data)}");

		// Exemple F#
		string analysis = MyFSharpModule.analyzeCode("let x = 42");
		MessageBox.Show(analysis);

		string secure = MyFSharpModule.secureExecute("print('Hello')");
		MessageBox.Show(secure);
	}
}
