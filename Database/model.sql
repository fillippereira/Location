CREATE TABLE [dbo].[__EFMigrationsHistory] (
    [MigrationId]    NVARCHAR (150) NOT NULL,
    [ProductVersion] NVARCHAR (32)  NOT NULL
);

GO
CREATE TABLE [dbo].[Cliente] (
    [ClienteId]  UNIQUEIDENTIFIER NOT NULL,
    [Nome]       NVARCHAR (MAX)   NOT NULL,
    [Sexo]       NVARCHAR (MAX)   NOT NULL,
    [Nascimento] DATETIME2 (7)    NOT NULL,
    [Cpf]        NVARCHAR (11)    NOT NULL,
    [Email]      NVARCHAR (MAX)   NOT NULL,
    [Celular]    NVARCHAR (MAX)   NOT NULL,
    [Endereco]   NVARCHAR (MAX)   NOT NULL,
    [Numero]     NVARCHAR (MAX)   NOT NULL,
    [CEP]        NVARCHAR (MAX)   NOT NULL,
    [Bairro]     NVARCHAR (MAX)   NOT NULL,
    [Cidade]     NVARCHAR (MAX)   NOT NULL,
    [Uf]         NVARCHAR (MAX)   NOT NULL
);

GO
CREATE TABLE [dbo].[Contrato] (
    [ContratoId] UNIQUEIDENTIFIER NOT NULL,
    [ImovelId]   UNIQUEIDENTIFIER NOT NULL,
    [ClienteId]  UNIQUEIDENTIFIER NOT NULL,
    [DataInicio] DATETIME2 (7)    NOT NULL,
    [DataFim]    DATETIME2 (7)    NOT NULL,
    [Valor]      REAL             NOT NULL
);

GO
CREATE TABLE [dbo].[Foto] (
    [FotoId]   UNIQUEIDENTIFIER NOT NULL,
    [ImovelId] UNIQUEIDENTIFIER NOT NULL,
    [Caminho]  NVARCHAR (MAX)   NULL
);

GO
CREATE TABLE [dbo].[Imovel] (
    [ImovelId]  UNIQUEIDENTIFIER NOT NULL,
    [Nome]      NVARCHAR (MAX)   NOT NULL,
    [TipoId]    UNIQUEIDENTIFIER NOT NULL,
    [QUartos]   INT              NOT NULL,
    [Banheiros] INT              NOT NULL,
    [Vagas]     INT              NOT NULL,
    [Area]      REAL             NOT NULL,
    [Endereco]  NVARCHAR (MAX)   NOT NULL,
    [Numero]    NVARCHAR (MAX)   NOT NULL,
    [CEP]       NVARCHAR (MAX)   NOT NULL,
    [Bairro]    NVARCHAR (MAX)   NOT NULL,
    [Cidade]    NVARCHAR (MAX)   NOT NULL,
    [Uf]        NVARCHAR (MAX)   NOT NULL
);

GO
CREATE TABLE [dbo].[Tipo] (
    [TipoId]     UNIQUEIDENTIFIER NOT NULL,
    [TipoImovel] NVARCHAR (MAX)   NULL
);

GO
ALTER TABLE [dbo].[Contrato]
    ADD CONSTRAINT [FK_Contrato_Cliente_ClienteId] FOREIGN KEY ([ClienteId]) REFERENCES [dbo].[Cliente] ([ClienteId]) ON DELETE CASCADE;

GO
ALTER TABLE [dbo].[Contrato]
    ADD CONSTRAINT [FK_Contrato_Imovel_ImovelId] FOREIGN KEY ([ImovelId]) REFERENCES [dbo].[Imovel] ([ImovelId]) ON DELETE CASCADE;

GO
ALTER TABLE [dbo].[Foto]
    ADD CONSTRAINT [FK_Foto_Imovel_ImovelId] FOREIGN KEY ([ImovelId]) REFERENCES [dbo].[Imovel] ([ImovelId]) ON DELETE CASCADE;

GO
ALTER TABLE [dbo].[Imovel]
    ADD CONSTRAINT [FK_Imovel_Tipo_TipoId] FOREIGN KEY ([TipoId]) REFERENCES [dbo].[Tipo] ([TipoId]) ON DELETE CASCADE;

GO
ALTER TABLE [dbo].[__EFMigrationsHistory]
    ADD CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED ([MigrationId] ASC);

GO
ALTER TABLE [dbo].[Cliente]
    ADD CONSTRAINT [PK_Cliente] PRIMARY KEY CLUSTERED ([ClienteId] ASC);

GO
ALTER TABLE [dbo].[Contrato]
    ADD CONSTRAINT [PK_Contrato] PRIMARY KEY CLUSTERED ([ContratoId] ASC);

GO
ALTER TABLE [dbo].[Foto]
    ADD CONSTRAINT [PK_Foto] PRIMARY KEY CLUSTERED ([FotoId] ASC);

GO
ALTER TABLE [dbo].[Imovel]
    ADD CONSTRAINT [PK_Imovel] PRIMARY KEY CLUSTERED ([ImovelId] ASC);

GO
ALTER TABLE [dbo].[Tipo]
    ADD CONSTRAINT [PK_Tipo] PRIMARY KEY CLUSTERED ([TipoId] ASC);

GO
CREATE NONCLUSTERED INDEX [IX_Contrato_ClienteId]
    ON [dbo].[Contrato]([ClienteId] ASC);

GO
CREATE NONCLUSTERED INDEX [IX_Contrato_ImovelId]
    ON [dbo].[Contrato]([ImovelId] ASC);

GO
CREATE NONCLUSTERED INDEX [IX_Foto_ImovelId]
    ON [dbo].[Foto]([ImovelId] ASC);

GO
CREATE NONCLUSTERED INDEX [IX_Imovel_TipoId]
    ON [dbo].[Imovel]([TipoId] ASC);

GO
