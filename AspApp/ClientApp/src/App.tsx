import "./bootstrap.js";
//import React from "react";
import { createRoot } from "react-dom/client";
import { createInertiaApp } from "@inertiajs/react";
import { InertiaProgress } from "@inertiajs/progress";

import { resolvePageComponent } from "laravel-vite-plugin/inertia-helpers";
import {StrictMode} from "react";
import "./App.css"

const appName =
    window.document.getElementsByTagName("title")[0]?.innerText || "Zibumakin Software";

createInertiaApp({
    title: (title) => `${title} - ${appName}`,
    resolve: (name) =>
        resolvePageComponent(
            `./pages/${name}.tsx`,
            import.meta.glob("./pages/**/*.tsx")
        ),
    
    setup({ el, App, props }) {
        const root = createRoot(el);
        console.log("rendering on : ", root);
        console.log("  -App : ", App);
        console.log("  -props : ", props);
        root.render(<StrictMode><App {...props} /></StrictMode>);
    },
}).then(() => {
    console.log("Inertia App Rendered!");
});


InertiaProgress.init({
    color: "#f00",
    delay: 50,
    showSpinner: true,
})
