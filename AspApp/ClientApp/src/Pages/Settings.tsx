import {Head, Link} from "@inertiajs/react";

export interface SettingsModel
{
}

export default function SettingsPage(model: SettingsModel) {
    console.log(model);
    return (
        <div>
            <Head title="설정"/>
            <h1 className="">설정</h1>
            <nav>
                <ul className={"list-disc list-inside"}>
                    <li><Link href="/">홈페이지</Link></li>
                    <li><Link href="/users">사용자정보</Link></li>
                    <li><Link href="/settings">설정</Link></li>
                </ul>
            </nav>
        </div>
    );
}

// IndexPage.layout = (page) => <Layout title="Home" children={page} />;
