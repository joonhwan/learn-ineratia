import "./bootstrap";
//import React from "react";
import { createRoot } from "react-dom/client";
import { createInertiaApp } from "@inertiajs/react";
import { resolvePageComponent } from "laravel-vite-plugin/inertia-helpers";
import {StrictMode} from "react";
import "./App.css"

const appName =
    window.document.getElementsByTagName("title")[0]?.innerText || "Zibumakin Software";

createInertiaApp({
    title: (title) => `${title} - ${appName}`,
    resolve: (name) =>
        resolvePageComponent(
            `./Pages/${name}.tsx`,
            import.meta.glob("./Pages/**/*.tsx")
        ),
    
    setup({ el, App, props }) {
        const root = createRoot(el);
        console.log("rendering on : ", root, App, props)
        root.render(<StrictMode><App {...props} /></StrictMode>);
    },
});
