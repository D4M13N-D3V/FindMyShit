# Personal Data Tool

## Overview
This project is a C# application designed for indexing personal documents and messages across platforms and devices, making them easily taggable and searchable. It integrates with Meilisearch for indexing, Tika for text extraction, and includes features for migrations and classification.

## Known Problems
When running this in your IDE on mac I always use Rider. I run into problems with the binary for Meilisearch binary not being trusted by mac. You will have to build the application and then navigate into the /PDT.CLI/bin/net8.0/ folder and run the `xattr -d com.apple.quarantine meilisearch` command.

## Features
- **Repository Management**: Add, remove, and manage repositories.
- **Document Management**: Add and remove documents within repositories.
- **Text Extraction**: Extract text from documents using Tika.
- **Cross-Platform Indexing**: Index documents and messages from various platforms and devices.
- **Tagging and Searching**: Easily tag and search through indexed content.
- **Migrations**: Support for migrating data between different versions or systems.
- **Classification**: Classify documents and messages for better organization and retrieval.

## Prerequisites
- .NET 8.0
- Mac Silicone
- Mac OS

## Technology Stack
- **Meilisearch**: For indexing and searching documents.
- **TikaOnDotNet**: For text extraction from documents.
- **MAUI**: For building cross-platform applications.

## Installation
1. Clone the repository:
    ```sh
    git clone https://github.com/yourusername/your-repo.git
    ```
2. Navigate to the project directory:
    ```sh
    cd your-repo
    ```
3. Restore the dependencies:
    ```sh
    dotnet restore
    ```
4. Install the binary from https://github.com/meilisearch/meilisearch/releases/tag/v1.10.2 into /PDT/meilisearch (meilisearch being the file name).

## Configuration
1. Configure Meilisearch settings in `appsettings.json`:
    ```json
    {
      "MeiliConfiguration": {
        "MeiliRepositoryIndex": "your-repository-index"
      }
    }
    ```
2. Configure Tika settings in `appsettings.json`:
    ```json
    {
      "TikaConfiguration": {
        "TikaThreadPoolSize": 5
      }
    }
    ```

## Logging
The application uses `Microsoft.Extensions.Logging` for logging. Configure the logging settings in `appsettings.json` as needed.

## Contributing
1. Fork the repository.
2. Create a new branch (`git checkout -b feature-branch`).
3. Commit your changes (`git commit -am 'Add new feature'`).
4. Push to the branch (`git push origin feature-branch`).
5. Create a new Pull Request.

## License
This project is licensed under the MIT License. See the `LICENSE` file for details.