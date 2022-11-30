// reacstrap documentation : https://6-4-0--reactstrap.netlify.app/components/carousel/

import React, { useState } from 'react';
import {
    Carousel,
    CarouselItem,
    CarouselIndicators,
    CarouselCaption,
    Container,
    Row
} from 'reactstrap';

import img1 from 'assets/images/intro/img1.png';
import img2 from 'assets/images/intro/img1.png';
import img3 from 'assets/images/intro/img1.png';

const items = [
    {
        src: img1,
        altText: '',
        caption: ''
    },
    {
        src: img2,
        altText: '',
        caption: ''
    },
    {
        src: img3,
        altText: '',
        caption: ''
    }
];

const JsComponents = (props) => {

    const [activeIndex, setActiveIndex] = useState(0);
    const [animating, setAnimating] = useState(false);

    const next = () => {
        if (animating) return;
        const nextIndex = activeIndex === items.length - 1 ? 0 : activeIndex + 1;
        setActiveIndex(nextIndex);
    }

    const previous = () => {
        if (animating) return;
        const nextIndex = activeIndex === 0 ? items.length - 1 : activeIndex - 1;
        setActiveIndex(nextIndex);
    }

    const goToIndex = (newIndex) => {
        if (animating) return;
        setActiveIndex(newIndex);
    }

    const slides = items.map(item => (
        <CarouselItem
            className="custom-tag"
            onExiting={() => setAnimating(true)}
            onExited={() => setAnimating(false)}
            key={item.src}
        >
            <img className="w-100" src={item.src} alt={item.altText} />
            <CarouselCaption captionText={item.caption} captionHeader={item.caption} />
        </CarouselItem>
    ));

    return (
        <div style={{border:"10px"}}>
            <Container>
                <h4 className="card-title mb-3 fw-bold">&nbsp;&nbsp;농사 & 목축</h4>
                <h6 className="card-subtitle mb-3"><code>&nbsp;&nbsp;&nbsp;8가지 종류의 계절별 작물을 재배하고 <br/>&nbsp;&nbsp;&nbsp;가축을 길러 자원을 얻으세요.</code></h6>
                <Row>
                    <Carousel
                        activeIndex={activeIndex}
                        next={next.bind(null)}
                        previous={previous.bind(null)}
                        interval={5000}
                        ride={'carousel'}
                    >
                        <CarouselIndicators items={items} activeIndex={activeIndex} onClickHandler={goToIndex.bind(null)} />
                        {slides}
                        {/* <CarouselControl direction="prev" directionText="Previous" onClickHandler={previous.bind(null)} /> */}
                        {/* <CarouselControl direction="next" directionText="Next" onClickHandler={next.bind(null)} /> */}
                    </Carousel>
                </Row>
            </Container>
            <p className="m-t-15 m-b-0"></p>
        </div>
    );
}

export default JsComponents;
