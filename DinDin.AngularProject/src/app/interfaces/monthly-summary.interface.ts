import { Transaction } from "./transaction.interface"

export interface MonthlySummary {
    id: number
    month: number
    year: number
    totalIncome: number
    totalExpense: number
    userId: string
    balance: number
    transactions: Transaction[]
}