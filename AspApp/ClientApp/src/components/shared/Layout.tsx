import {clsx} from "clsx";
import {Link, usePage} from "@inertiajs/react";
import {routes} from "@/routes.ts";
import {ThemeProvider} from "@/components/shared/ThemeContext.tsx";

export default function Layout(props: any) {
    const page = usePage();
    return (
        <ThemeProvider>
            <div className={""}>
                <nav className={"flex justify-between items-center h-16 px-4 mb-4 bg-primary text-primary-content"}>
                    <h1 className={"text-2xl font-bold"}>Zibumakin Software</h1>
                    <ul className={"flex gap-4"}>
                        {
                            routes.map((route, index) => {
                                const isActive = page.component === route.name;
                                const cls = clsx("font-bold", isActive ? "underline" : "");
                                console.log("route : ", route.name, cls);
                                return <li key={index}>
                                    <Link href={route.path}
                                          className={cls}>{route.title}</Link>
                                </li>
                            })
                        }
                        <li><Link href="/logout" method={"post"} data={{foo: "bar"}}>로그아웃</Link></li>
                    </ul>
                </nav>
                <main className={"container m-auto"}>
                    {props.children}
                </main>
            </div>
        </ThemeProvider>
    );
}