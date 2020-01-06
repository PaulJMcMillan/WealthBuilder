CREATE TABLE [dbo].[TransferTransactions] (
    [Id]            INT           NOT NULL,
    [Date]          DATE          NULL,
    [Description]   VARCHAR (255) NULL,
    [Deposit]       MONEY         NULL,
    [Withdrawal]    MONEY         NULL,
    [Cleared]       BIT           NULL,
    [CheckNumber]   INT           NULL,
    [Reconciled]    BIT           NULL,
    [Notes]         VARCHAR (255) NULL,
    [AccountId]     INT           NULL,
    [EntityId]      INT           NULL,
    [TaxFormId]     INT           NULL,
    [TaxCategoryId] INT           NULL,
    [CategoryId]    INT           NULL,
    [AssetId]       INT           NULL,
    [ContractorId]  INT           NULL,
    CONSTRAINT [PK_TransferTransactions] PRIMARY KEY CLUSTERED ([Id] ASC)
);

