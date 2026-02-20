# Semantic Kernel Overview

Semantic Kernel is an open-source SDK from Microsoft that enables developers to integrate large language models into .NET, Python, and Java applications. It provides abstractions for AI services, plugins, agents, and memory.

## Core Concepts

The Kernel is the central object in Semantic Kernel. It holds registered AI services and plugins. You build it using Kernel.CreateBuilder() and then call Build().

Plugins are collections of KernelFunctions that can be invoked by the Kernel or by AI agents. A plugin can be a C# class with [KernelFunction] attributes, or an inline prompt function created with KernelFunctionFactory.CreateFromPrompt().

## Agents

ChatCompletionAgent is the primary agent type for conversational tasks. It wraps a chat completion service and can call plugins autonomously using FunctionChoiceBehavior.Auto(). The agent loop continues until the model returns a response without requesting any additional tool calls.

## Memory and Vector Stores

Semantic Kernel supports multiple vector store connectors including Azure AI Search, Qdrant, Chroma, and InMemory. The IVectorStore interface provides a consistent API for upserting and searching records regardless of the backend.

Text embeddings are generated using ITextEmbeddingGenerationService. Azure OpenAI provides text-embedding-ada-002 and text-embedding-3-small. OpenAI provides the same models via the non-Azure endpoint.

## Sequential Pipelines

For deterministic workflows, Kernel.InvokeAsync() can call KernelFunctions directly without an agent. KernelArguments passes typed context between steps. ResponseFormat = "json_object" in OpenAIPromptExecutionSettings ensures structured JSON output from each step.
