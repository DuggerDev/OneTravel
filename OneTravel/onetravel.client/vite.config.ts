import { fileURLToPath, URL } from 'node:url';
import { defineConfig } from 'vite';
import plugin from '@vitejs/plugin-react';
import fs from 'fs';
import path from 'path';
import child_process from 'child_process';
import { env } from 'process';

const baseFolder = env.APPDATA && env.APPDATA !== ''
    ? `${env.APPDATA}/ASP.NET/https`
    : `${env.HOME}/.aspnet/https`;

const certificateName = "onetravel.client";
const certFilePath = path.join(baseFolder, `${certificateName}.pem`);
const keyFilePath = path.join(baseFolder, `${certificateName}.key`);

// Try to generate certs (safe fallback if dotnet missing)
if (!fs.existsSync(certFilePath) || !fs.existsSync(keyFilePath)) {
    try {
        const result = child_process.spawnSync('dotnet', [
            'dev-certs', 'https', '--export-path', certFilePath,
            '--format', 'Pem', '--no-password'
        ], { stdio: 'inherit' });
        if (result.error) {
            console.warn("Skipping certificate creation: dotnet not found.");
        }
    } catch {
        console.warn("Skipping certificate creation: dotnet not found.");
    }
}

const target = env.ASPNETCORE_HTTPS_PORT
    ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}`
    : env.ASPNETCORE_URLS
        ? env.ASPNETCORE_URLS.split(';')[0]
        : 'https://localhost:7212';

// https://vitejs.dev/config/
export default defineConfig({
    plugins: [plugin()],  
    resolve: {
        alias: {
            '@': fileURLToPath(new URL('./src', import.meta.url))
        }
    },
    server: {
        proxy: {
            '^/weatherforecast': {
                target,
                secure: false
            }
        },
        port: 5173,
        ...(fs.existsSync(keyFilePath) && fs.existsSync(certFilePath) ? {
            https: {
                key: fs.readFileSync(keyFilePath),
                cert: fs.readFileSync(certFilePath),
            }
        } : {}) // <-- if no certs, no https field
    }
});
