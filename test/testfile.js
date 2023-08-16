require("chromedriver");
const { Builder, until, By, Key } = require("selenium-webdriver");
var should = require("chai").expect();

describe("Testing the train tab", async() => {



  it("Cannot add existing number and different train attributes ", async () => {

    ////*[@id="root"]/div[2]/div/div/div[2] /html/body/div/div[2]/section/form/div/div[6]/button
    let driver = await new Builder().forBrowser("chrome").build();

    await driver.get("http://localhost:3000");

    await driver.findElement(By.xpath('//*[@id="root"]/div[1]/div/div/div/div/ul/li[1]/a')).sendKeys("Trains", Key.RETURN);

    await driver.findElement(By.xpath('//*[@id=":r1:"]')).sendKeys("200");

    await driver.findElement(By.xpath('//*[@id=":r3:"]')).sendKeys("Yaal Devi");

    await driver.findElement(By.xpath('//*[@id="root"]/div[2]/div[1]/div/div/div/form/div[3]/div/div')).sendKeys(Key.RETURN);

    await driver.findElement(By.xpath('//*[@id="menu-"]/div[3]/ul/li[3]')).sendKeys(Key.RETURN);
    
    await driver.findElement(By.xpath('//*[@id="root"]/div[2]/div[1]/div/div/div/form/button')).sendKeys("ADD TRAIN",Key.RETURN);

    

    setInterval(async()=>{

        let textSubmit  = await driver.findElement(By.xpath('//*[@id="root"]/div[2]/div[1]/div/div/div/div/div/div[2]')).getText()
        .then((value)=>{
          
            return value;
        })

        console.log(textSubmit);
        await driver.close();
        textSubmit.should.equal("submition error");
        

    },2000)

    

  });



 it("Adding new train", async () => {

    ////*[@id="root"]/div[2]/div/div/div[2] /html/body/div/div[2]/section/form/div/div[6]/button
    let driver = await new Builder().forBrowser("chrome").build();

    await driver.get("http://localhost:3000");
    await driver.findElement(By.xpath('//*[@id="root"]/div[1]/div/div/div/div/ul/li[1]/a')).sendKeys("Trains", Key.RETURN);

    await driver.findElement(By.xpath('//*[@id=":r1:"]')).sendKeys("708");

    await driver.findElement(By.xpath('//*[@id=":r3:"]')).sendKeys("Yaal Devi");

    await driver.findElement(By.xpath('//*[@id="root"]/div[2]/div[1]/div/div/div/form/div[3]/div/div')).sendKeys(Key.RETURN);

    await driver.findElement(By.xpath('//*[@id="menu-"]/div[3]/ul/li[3]')).sendKeys(Key.RETURN);
    
    await driver.findElement(By.xpath('//*[@id="root"]/div[2]/div[1]/div/div/div/form/button')).sendKeys("ADD TRAIN",Key.RETURN);

    

    setInterval(async()=>{

        let textSubmit  = await driver.findElement(By.xpath('//*[@id="root"]/div[2]/div[1]/div/div/div/div/div/div[2]')).getText()
        .then(async(value)=>{
            console.log(textSubmit);
            await driver.close();
            
            
            return value;
        })

        textSubmit.should.equal("Train added successfully.");
        
        
        


    },3000)

    


  })


  it("Receiving the trains", async () => {

    ////*[@id="root"]/div[2]/div/div/div[2] /html/body/div/div[2]/section/form/div/div[6]/button
    let driver = await new Builder().forBrowser("chrome").build();

    await driver.get("http://localhost:3000");

    await driver.findElement(By.xpath('//*[@id="root"]/div[1]/div/div/div/div/ul/li[1]/a')).sendKeys("Trains", Key.RETURN);

    await driver.findElement(By.xpath('//*[@id="root"]/div[2]/header/div/div/div/button[2]')).sendKeys("Available Trains", Key.RETURN);

    setInterval(
      async()=>{
        let receivedText = await driver.findElement(By.xpath('//*[@id="root"]/div[2]/div[2]/div/div/div/div/div/div[1]/div[1]/h6')).getText().then(async(v)=>{
          

        console.log(v);
              await driver.close();
    
              receivedText.should.equal("Train Number: 708");
    
              return v;  
              
        });
      },2000
    )
   
  

  


  })



 


 


});

