import { Injectable } from '@angular/core';
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const token = localStorage.getItem('token');

        const isAuthRoute = req.url.includes('login') || req.url.includes('register');

        if (token && !isAuthRoute) {
            const clonedReq = req.clone({
                headers: req.headers.set('Authorization', `Bearer ${token}`)
            });

            return next.handle(clonedReq);
        }

        return next.handle(req);
    }
}