import { Transaction } from "./transaction.interface"

export interface MonthlySummary {
    id: string
    month: any
    year: number
    totalIncome: number
    totalExpense: number
    userId: string
    balance: number
    transactions: Transaction[]
}