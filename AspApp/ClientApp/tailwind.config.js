/** @type {import('tailwindcss').Config} */
export default {
  content: [
    "./src/**/*.{js,jsx,ts,tsx}",
  ],
  darkMode: 'class',
  theme: {
    extend: {
      colors: ({colors}) => ({
        // 배경색과 텍스트 색상 정의
        background: 'var(--background)',
        foreground: 'var(--foreground)',
        // 주요 색상 정의
        primary: 'var(--primary)',
        "primary-content": 'var(--primary-content)',
        // 보조 색상 정의
        secondary: 'var(--secondary)',
        // 주요 색상 정의
        accent: 'var(--accent)',
      })
    },
  },
  plugins: [],
}

