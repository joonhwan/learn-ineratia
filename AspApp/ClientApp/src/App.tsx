import "./App.css"
import "./bootstrap.js";

import {createRoot} from "react-dom/client";
import {createInertiaApp} from "@inertiajs/react";
import {InertiaProgress} from "@inertiajs/progress";

import {resolvePageComponent} from "laravel-vite-plugin/inertia-helpers";
import {StrictMode} from "react";
import {ThemeProvider} from "@/components/shared/ThemeContext.tsx";
import Layout from "@/components/shared/Layout.tsx";

// const appName =
//     window.document.getElementsByTagName("title")[0]?.innerText || "InertiaApp";
const appName = "InertiaApp";

createInertiaApp({
    title: (title) => title ? `${appName} - ${title}` : `${appName}`,
    
    resolve:  async (name) => {
        const component: any = await resolvePageComponent(
            `./pages/${name}.tsx`,
            import.meta.glob("./pages/**/*.tsx")
        );
        console.log("@component : ", component);
        component.default.layout = component.default.layout || ((page:any) => (<Layout children={page} />))
        return component
    },

    setup: ({el, App, props}) => {
        const root = createRoot(el);
        // console.log("rendering on : ", root);
        // console.log("  - app : ", App);
        // console.log("  - props : ", props);
        root.render(
            <StrictMode>
                <ThemeProvider>
                    <App {...props} />
                </ThemeProvider>
            </StrictMode>
        )
    },
}).then(() => {
    console.log("Inertia App Rendered!");
});


InertiaProgress.init({
    color: "#d92",
    delay: 50,
    includeCSS: true,
    showSpinner: true,
})
