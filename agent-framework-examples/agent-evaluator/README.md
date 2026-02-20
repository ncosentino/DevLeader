# AI Agent Evaluation Harness

A .NET 10 console application demonstrating programmatic evaluation of AI agent quality using LLM-as-judge pattern with Microsoft.Extensions.AI.

## Overview

This application provides a harness for evaluating AI agents against a suite of test scenarios. It tests agent responses across multiple dimensions including intent resolution, task adherence, factual accuracy, helpfulness, and safety.

## Architecture

The application consists of several key components:

- **EvaluationHarness**: Orchestrates the evaluation process
- **SubjectAgent**: The AI agent being evaluated (configured as a helpful C#/.NET assistant)
- **LlmJudgeEvaluator**: Uses an LLM to score responses against defined criteria
- **ScenarioDefinitions**: Defines test scenarios covering different evaluation dimensions
- **ReportWriter**: Generates JSON and Markdown reports

## Prerequisites

- .NET 10 SDK
- OpenAI API key or Azure OpenAI endpoint
- Internet connection for API calls

## Setup

1. Clone or download this project

2. Configure your AI provider in `appsettings.Development.json`:

```json
{
  "AIProvider": {
    "Type": "openai",
    "ModelId": "gpt-4o-mini",
    "ApiKey": "your-api-key-here"
  }
}
```

For Azure OpenAI:
```json
{
  "AIProvider": {
    "Type": "azureopenai",
    "ModelId": "gpt-4o-mini",
    "ApiKey": "your-api-key-here",
    "Endpoint": "https://your-resource.openai.azure.com"
  }
}
```

3. Restore packages:
```bash
dotnet restore
```

4. Build the project:
```bash
dotnet build
```

## Usage

Run the evaluation harness:

```bash
dotnet run
```

The application will:
1. Load the predefined evaluation scenarios
2. Run each scenario through the subject agent
3. Evaluate responses using the LLM-as-judge pattern
4. Display results in the console
5. Save detailed reports to the `reports/` directory

### Example Output

```
🧪 AI Agent Evaluation Harness
Running 5 evaluation scenarios...

[1/5] Intent Resolution: "Explain what dependency injection is in C#"
  ✅ Score: 8.5/10 (Passed)
     Response demonstrates clear understanding of DI with relevant C# examples

[2/5] Task Adherence: "Give me 3 C# code examples of the singleton pattern"
  ✅ Score: 9.0/10 (Passed)
     Provides exactly 3 distinct, valid singleton implementations

...

📊 Evaluation Results Summary:
Overall Quality Score: 8.4/10
Passed: 5/5 scenarios

Detailed reports saved to:
  - reports/eval-20240315-143022.json
  - reports/eval-20240315-143022.md
```

## Evaluation Scenarios

The harness includes 5 default scenarios:

1. **Intent Resolution**: Tests ability to explain DI in C#
2. **Task Adherence**: Tests ability to provide specific number of code examples
3. **Factual Accuracy**: Tests knowledge of C# language features
4. **Helpfulness**: Tests practical guidance for .NET setup
5. **Safety**: Tests safe explanation of security concepts

## Evaluation Criteria

Each scenario is evaluated against multiple criteria:
- Clarity and structure of response
- Factual accuracy
- Adherence to specific requirements
- Practical value
- Safety considerations

The LLM judge scores responses on a scale of 1-10. The default pass threshold is 6.0.

## Reports

Two report formats are generated:

### JSON Report (`reports/eval-{timestamp}.json`)
Structured data containing:
- Overall statistics
- Individual scenario results
- Full question/response/reasoning for each test

### Markdown Report (`reports/eval-{timestamp}.md`)
Human-readable format with:
- Summary statistics
- Detailed results for each scenario
- Easy-to-share documentation

## Configuration

Edit `appsettings.json` to customize:

- `Evaluation:EvaluatorModelId`: Model to use for evaluation (can differ from subject agent)
- `Evaluation:PassThreshold`: Minimum score for passing (default: 6.0)
- `Evaluation:OutputDirectory`: Where to save reports (default: "reports")

## Evaluation Approach

This implementation uses the **LLM-as-judge** pattern due to the current unavailability of `Microsoft.Extensions.AI.Evaluation` packages for .NET 10. The approach:

1. Defines clear evaluation criteria for each scenario
2. Uses a separate LLM instance as an objective evaluator
3. Parses structured evaluation output (score + reasoning)
4. Applies consistent scoring across all scenarios

This pattern is widely used in AI evaluation and provides:
- Consistent evaluation criteria
- Detailed reasoning for scores
- Flexibility to evaluate any aspect of responses
- No dependency on external evaluation libraries

## Extending

### Adding New Scenarios

Edit `Evaluation/ScenarioDefinitions.cs`:

```csharp
new EvaluationScenario
{
    Name = "Your Scenario Name",
    UserMessage = "The prompt to test",
    ExpectedBehavior = "What should happen",
    EvaluationCriteria = new List<string>
    {
        "Criterion 1",
        "Criterion 2"
    }
}
```

### Customizing the Subject Agent

Edit `Agents/SubjectAgent.cs` to change the agent's instructions or behavior.

### Customizing Evaluation Logic

Edit `Evaluation/LlmJudgeEvaluator.cs` to modify scoring logic or evaluation prompts.

## Troubleshooting

**API Key Not Found**
- Ensure `appsettings.Development.json` exists and contains your API key
- Check that the file is not being ignored by git

**Low Scores**
- Review individual scenario reasoning in the markdown report
- Consider adjusting evaluation criteria
- May need to improve subject agent instructions

**Build Errors**
- Ensure .NET 10 SDK is installed
- Run `dotnet restore` to ensure all packages are downloaded
- Check that all required files are present

## License

This is a demonstration project for educational purposes.
