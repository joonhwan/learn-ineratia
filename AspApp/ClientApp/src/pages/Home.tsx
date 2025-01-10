import { Head, Link, usePage } from "@inertiajs/react";
import { ReactNode} from "react";

export interface HomeModel
{
    title: string
    time: string
}

export default function HomePage(model: HomeModel) : ReactNode {
    const page = usePage();
    console.log("page : ", page);
    return (
        <>
            <Head>
                <title> "홈페이지" </title>
                <meta name="description" content="Home 에 대한 정보" head-key={"description"}/>
            </Head>
                <h1 className="">홈페이지</h1>

                <div className={"timecode"}>
                    <p>{model.time}</p>
                    <Link href="/" preserveScroll
                          className={"py-1 px-4 rounded-md"}>Refresh</Link>
                </div>
            </>
            );
            }

            //HomePage.layout = (page: React.ReactNode) => <Layout children={page} />;
