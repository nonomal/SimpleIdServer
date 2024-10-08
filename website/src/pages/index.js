import React from 'react';
import clsx from 'clsx';
import Link from '@docusaurus/Link';
import useDocusaurusContext from '@docusaurus/useDocusaurusContext';
import Layout from '@theme/Layout';
import HomepageFeatures from '@site/src/components/HomepageFeatures';
import HomepageInstallations from '@site/src/components/HomepageInstallations';
import styles from './index.module.css';
import HomepagePhilosophy from '../components/HomepagePhilosophy';
import HomepageOpenStandards from '../components/HomepageOpenStandards';

function HomepageHeader() {
  const {siteConfig} = useDocusaurusContext();
  return (
    <header className={clsx('hero hero--primary', styles.sidBanner)}>
      <div className="container">
        <h1 className="hero__title">{siteConfig.title}</h1>
        <p className="hero__subtitle">{siteConfig.tagline}</p>
        <div className={styles.link}>
          <Link
            className="button button--secondary button--lg"
            to="https://website.simpleidserver.com/master/clients">
            Identity server  
          </Link>
          <Link
            className="button button--secondary button--lg"
            to="https://credentialissuerwebsite.simpleidserver.com">
            Credential issuer
          </Link>
          <Link
            className="button button--secondary button--lg"
            to="https://install.appcenter.ms/users/agentsimpleidserver-gmail.com/apps/simpleidserver/distribution_groups/public">
              Mobile
          </Link>
          <div style={{textAlign:"center", paddingTop: 10}}>
              <a href="https://play.google.com/store/apps/details?id=com.simpleidserver.mobile&pcampaignid=web_share"><img src="/img/play-store.png" style={{width: 250}} /></a>
              <a href="https://apps.apple.com/be/app/simpleidserver/id6647993628?l=fr-FR"><img src="/img/app-store.png"  style={{width: 250, paddingLeft: 5}} /></a>
          </div>
        </div>
      </div>
    </header>
  );
}

export default function Home() {
  const {siteConfig} = useDocusaurusContext();
  return (
    <Layout
      title={`Hello from ${siteConfig.title}`}
      description="Description will go into a meta tag in <head />">
      <HomepageHeader />
      <main>
        <div>
          <HomepagePhilosophy />
        </div>
        <div style={{backgroundColor: "#f5f6f7"}}>
          <HomepageInstallations />
        </div>
        <div>
          <HomepageFeatures />
        </div>
        <div style={{backgroundColor: "#f5f6f7"}}>
          <HomepageOpenStandards />
        </div>
      </main>
    </Layout>
  );
}
