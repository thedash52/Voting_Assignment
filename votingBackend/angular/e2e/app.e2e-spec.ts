import { votingBackendTemplatePage } from './app.po';

describe('abp-project-name-template App', function() {
  let page: votingBackendTemplatePage;

  beforeEach(() => {
    page = new votingBackendTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
