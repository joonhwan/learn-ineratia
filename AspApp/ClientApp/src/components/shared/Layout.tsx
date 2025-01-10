import {clsx} from "clsx";
import {Head, Link} from "@inertiajs/react";
import {routes} from "@/routes.ts";
import {useAppPage} from "@/components/shared/sharedData.ts";


export default function Layout({children}: any) {
    const { component, props} = useAppPage();
    
    return (
        <div className={"pt-1"}>
            <Head>
                <title>"Inertia App"</title>
                <meta name="description" content="Inertia App 에 대한 정보" head-key={"description"}/>
            </Head>
            <nav className={"flex justify-between items-center h-16 px-4 mb-4 bg-primary text-primary-content"}>
                <div className={"flex flex-col"}>
                    <h1 className={"text-2xl font-bold"}>Zibumakin Software</h1>
                    <p className={"text-sm"}>Welcome {props.auth.user.name}!</p>
                </div>
                <ul className={"flex gap-4"}>
                    {
                        routes.map((route, index) => {
                            const isActive = component === route.name;
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
                {children}
            </main>
        </div>
    );
}