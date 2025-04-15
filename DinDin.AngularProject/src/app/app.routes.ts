import { Routes } from "@angular/router";
import { LoginComponent } from "./pages/login/login.component";
import { MonthlySummariesListComponent } from "./pages/monthly-summaries-list/monthly-summaries-list.component";

export const routes: Routes = [
    {
        path: "login",
        component: LoginComponent
    },
    {
        path: "list",
        component: MonthlySummariesListComponent
    }
];