export interface Transaction {
    type: number;
    incomeCategory: number;
    expenseCategory: number;
    amont: number;
    description: string;
    transactionDate: string;
    monthlySummaryId: number
} 